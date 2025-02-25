using UnityEngine;

public class PlayerController : NamMonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public bool onGround;
    [SerializeField] public bool isAttacking;
    [SerializeField] public bool isJumping;
    [SerializeField] private int maxHealth = 100; // Máu tối đa
    [SerializeField] private int currentHealth; // Máu hiện tại
    private GameObject meleeHitbox; // Biến private cho hitbox
    [SerializeField] private MeleeHitbox meleeHitboxScript; // Tham chiếu đến script MeleeHitbox

    public PlayerStateMachine playerStateMachine;
    private PlayerIdleState idleState;
    private PlayerRunState runState;
    private PlayerAttackState attackState;
    private PlayerJumpState jumpState;
    private PlayerHurtState hurtState;

    private Vector2 VectorToRight = Vector2.right;
    private Vector2 VectorToLeft = Vector2.left;
    private Vector2 VectorToUp = Vector2.up;

    // Thêm biến cho thời gian bất tử
    private float invincibilityDuration = 2f; // Thời gian bất tử 2 giây
    private float invincibilityTimer; // Đếm ngược thời gian bất tử
    private bool isInvincible = false; // Trạng thái bất tử
    private SpriteRenderer spriteRenderer; // Để nhấp nháy khi bất tử

    // Thêm biến để theo dõi thời gian cooldown nhận sát thương
    private float damageCooldown = 1f; // Thời gian chờ 1 giây giữa các lần nhận sát thương
    private float lastDamageTime; // Thời điểm nhận sát thương gần nhất

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadPlayerInput();
        LoadAnimator();
        LoadRigidbody2D();
        LoadMeleeHitbox();
        LoadSpriteRenderer(); // Load SpriteRenderer để nhấp nháy
    }

    protected virtual void LoadPlayerInput()
    {
        if (playerInput != null) return;
        playerInput = GetComponentInChildren<PlayerInput>();
        if (playerInput == null) Debug.LogError("PlayerInput không tìm thấy trong " + transform.name);
    }

    protected virtual void LoadAnimator()
    {
        if (animator != null) return;
        animator = GetComponentInChildren<Animator>();
        if (animator == null) Debug.LogError("Animator không tìm thấy trong " + transform.name);
    }

    protected virtual void LoadRigidbody2D()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("Rigidbody2D không tìm thấy trong " + transform.name);
    }

    protected virtual void LoadMeleeHitbox()
    {
        if (meleeHitbox != null) return;
        meleeHitbox = transform.Find("MeleeHitbox")?.gameObject;
        if (meleeHitbox == null)
        {
            Debug.LogError("MeleeHitbox không tìm thấy trong " + transform.name);
            return;
        }
        meleeHitboxScript = meleeHitbox.GetComponent<MeleeHitbox>();
        if (meleeHitboxScript == null)
        {
            Debug.LogError("MeleeHitbox script không tìm thấy trong " + meleeHitbox.name);
        }
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (spriteRenderer != null) return;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer của Player
        if (spriteRenderer == null) Debug.LogWarning("SpriteRenderer không tìm thấy trong " + transform.name);
    }

    private void Start()
    {
        currentHealth = maxHealth; // Khởi tạo máu
        playerStateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(animator);
        runState = new PlayerRunState(animator);
        attackState = new PlayerAttackState(animator, playerInput);
        jumpState = new PlayerJumpState(animator);
        hurtState = new PlayerHurtState(animator);

        playerStateMachine.SetState(idleState);
        lastDamageTime = -damageCooldown; // Khởi tạo để có thể nhận sát thương ngay lập tức ban đầu
        invincibilityTimer = 0f; // Khởi tạo thời gian bất tử
    }

    private void Update()
    {
        HandleMovement();
        playerStateMachine.UpdateState();
        //HandleInvincibility(); // Cập nhật trạng thái bất tử
    }

    private void HandleMovement()
    {
        if (playerStateMachine.CurrentState is PlayerHurtState) return; // Không cho di chuyển khi bị Hurt

        if (playerInput.Jump && onGround)
        {
            if (isAttacking)
            {
                isAttacking = false;
            }
            PlayerJump(VectorToUp);
        }
        else if (playerInput.BowAttack && onGround && !isAttacking)
        {
            isAttacking = true;
            attackState.SetAttackType("Bow");
            playerStateMachine.SetState(attackState);
            Debug.Log("Bắt đầu tấn công Bow");
        }
        else if (playerInput.KnifeAttack && onGround && !isAttacking)
        {
            isAttacking = true;
            attackState.SetAttackType("Knife");
            playerStateMachine.SetState(attackState);
            Debug.Log("Bắt đầu tấn công Knife");
        }
        else if (playerInput.MoveRight)
        {
            if (isAttacking)
            {
                isAttacking = false;
            }
            PlayerMove(VectorToRight);
            RotatePlayer(false);
        }
        else if (playerInput.MoveLeft)
        {
            if (isAttacking)
            {
                isAttacking = false;
            }
            PlayerMove(VectorToLeft);
            RotatePlayer(true);
        }
        else if (onGround && !isJumping && !isAttacking)
        {
            playerStateMachine.SetState(idleState);
        }
    }

    public void TakeDamage(int damage)
    {
        // Kiểm tra cooldown và trạng thái bất tử trước khi nhận sát thương
        if (Time.time - lastDamageTime >= damageCooldown && !isInvincible)
        {
            currentHealth -= damage;
            Debug.Log($"Player nhận {damage} sát thương. Máu còn: {currentHealth}");
            lastDamageTime = Time.time; // Cập nhật thời gian nhận sát thương gần nhất
            //StartInvincibility(); // Bắt đầu thời gian bất tử

            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                // Kích hoạt animation "Hurt" ngay lập tức và chuyển sang trạng thái Hurt qua Animation Event
                StartHurtAnimation();
                animator.Play("Hurt");
            }
        }
        else
        {
            Debug.Log("Player đang trong cooldown hoặc bất tử, không nhận thêm sát thương!");
        }
    }

    private void Die()
    {
        Debug.Log("Player đã chết!");
        gameObject.SetActive(false); // Tạm thời vô hiệu hóa Player
        //Destroy(gameObject);
    }

    public void ResetAttackState()
    {
        isAttacking = false;
        Debug.Log("Đã reset trạng thái tấn công");
    }

    private void PlayerJump(Vector2 direction)
    {
        if (!onGround || isJumping) return;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        onGround = false;
        isJumping = true;
        playerStateMachine.SetState(jumpState);
    }

    private void RotatePlayer(bool flip)
    {
        transform.rotation = Quaternion.Euler(0, flip ? 180 : 0, 0);
        if (meleeHitbox != null)
        {
            // Xoay MeleeHitbox theo hướng của Player (localRotation)
            meleeHitbox.transform.localRotation = Quaternion.Euler(0, flip ? 180 : 0, 0);

            // Điều chỉnh vị trí hitbox theo hướng
            Vector3 hitboxPos = meleeHitbox.transform.localPosition;
            hitboxPos.x = Mathf.Abs(hitboxPos.x) * (flip ? -1 : 1);
            meleeHitbox.transform.localPosition = hitboxPos;

            // Điều chỉnh BoxCollider2D offset để khớp với hướng
            BoxCollider2D boxCollider = meleeHitbox.GetComponent<BoxCollider2D>();
            if (boxCollider != null)
            {
                boxCollider.offset = new Vector2(Mathf.Abs(boxCollider.offset.x) * (flip ? -1 : 1), boxCollider.offset.y);
            }
        }
    }

    private void PlayerMove(Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
        if (onGround)
        {
            playerStateMachine.SetState(runState);
        }
    }

    // Các hàm gọi từ Animation Event
    public void ActivateHitbox()
    {
        if (meleeHitboxScript != null)
        {
            meleeHitboxScript.ActivateHitbox();
            Debug.Log("PlayerController gọi ActivateHitbox");
        }
        else
        {
            Debug.LogError("meleeHitboxScript là null trong PlayerController");
        }
    }

    public void DeactivateHitbox()
    {
        if (meleeHitboxScript != null)
        {
            meleeHitboxScript.DeactivateHitbox();
            Debug.Log("PlayerController gọi DeactivateHitbox");
        }
        else
        {
            Debug.LogError("meleeHitboxScript là null trong PlayerController");
        }
    }

    public void StartHurtAnimation()
    {
        if (currentHealth > 0 && !(playerStateMachine.CurrentState is PlayerHurtState))
        {
            playerStateMachine.SetState(hurtState);
            Debug.Log("Player chuyển sang trạng thái Hurt qua Animation Event");
        }
    }

    public void EndHurtAnimation()
    {
        if (onGround && !(playerStateMachine.CurrentState is PlayerHurtState))
        {
            playerStateMachine.SetState(idleState);
            Debug.Log("Player thoát trạng thái Hurt và chuyển về Idle");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack") && !isInvincible) // Chỉ nhận sát thương nếu không bất tử
        {
            MonsterAttackHitbox monsterHitbox = collision.GetComponent<MonsterAttackHitbox>();
            if (monsterHitbox != null)
            {
                TakeDamage(10); // Gây 10 sát thương khi bị hitbox tấn công của quái chạm
            }
        }
    }

    //private void StartInvincibility()
    //{
    //    isInvincible = true;
    //    invincibilityTimer = invincibilityDuration;
    //    Debug.Log("Player trở nên bất tử trong " + invincibilityDuration + " giây");
    //    StartCoroutine(BlinkWhileInvincible()); // Bắt đầu nhấp nháy
    //}

    //private void HandleInvincibility()
    //{
    //    if (isInvincible)
    //    {
    //        invincibilityTimer -= Time.deltaTime;
    //        if (invincibilityTimer <= 0)
    //        {
    //            isInvincible = false;
    //            Debug.Log("Player hết thời gian bất tử");
    //            if (spriteRenderer != null)
    //            {
    //                spriteRenderer.color = Color.white; // Trở về màu ban đầu
    //            }
    //        }
    //    }
    //}

    //private System.Collections.IEnumerator BlinkWhileInvincible()
    //{
    //    if (spriteRenderer == null) yield break;

    //    float blinkInterval = 0.1f; // Khoảng thời gian nhấp nháy (0.1 giây)
    //    while (isInvincible)
    //    {
    //        spriteRenderer.color = spriteRenderer.color == Color.white ? Color.red : Color.white; // Nhấp nháy giữa trắng và đỏ
    //        yield return new WaitForSeconds(blinkInterval);
    //    }
    //    spriteRenderer.color = Color.white; // Trở về màu ban đầu khi hết bất tử
    //}
}