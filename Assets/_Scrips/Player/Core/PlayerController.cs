using UnityEngine;

public class PlayerController : NamMonoBehaviour // Giả sử NamMonoBehaviour là base class của bạn
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject meleeHitbox;
    [SerializeField] private MeleeHitbox meleeHitboxScript;
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private bool onGround;
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool facingRight = true;
    private PlayerStateMachine playerStateMachine;
    public PlayerStateMachine PlayerStateMachine => playerStateMachine;

    // Các state của nhân vật
    private PlayerIdleState idleState;
    private PlayerRunState runState;
    private PlayerAttackState attackState;
    private PlayerJumpState jumpState;
    private PlayerHurtState hurtState;

    // Properties để truy cập từ bên ngoài nếu cần
    public bool OnGround => onGround;
    public bool IsAttacking => isAttacking;
    public bool IsJumping => isJumping;

    // Thêm biến cho hiệu ứng bị đánh
    [SerializeField] private float hurtLockDuration = 0.5f; // Thời gian khóa di chuyển khi bị đánh (giây)
    private bool isHurtLocked;
    private float hurtLockTimer;
    [SerializeField] private float knockbackForce = 5f; // Lực hất lùi khi bị đánh

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadPlayerInput();
        LoadAnimator();
        LoadRigidbody2D();
        LoadMeleeHitbox();
        LoadPlayerHealth();
    }
    protected virtual void LoadPlayerHealth()
    {
        playerHealth = playerHealth ?? GetComponent<PlayerHealth>();
        if (playerHealth == null) Debug.LogError($"PlayerHealth không tìm thấy trong {transform.name}");
    }

    protected virtual void LoadPlayerInput()
    {
        playerInput = playerInput ?? GetComponentInChildren<PlayerInput>();
        if (playerInput == null) Debug.LogError($"PlayerInput không tìm thấy trong {transform.name}");
    }

    protected virtual void LoadAnimator()
    {
        animator = animator ?? GetComponentInChildren<Animator>();
        if (animator == null) Debug.LogError($"Animator không tìm thấy trong {transform.name}");
    }

    protected virtual void LoadRigidbody2D()
    {
        rb = rb ?? GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError($"Rigidbody2D không tìm thấy trong {transform.name}");
    }

    protected virtual void LoadMeleeHitbox()
    {
        if (meleeHitbox != null)
        {
            AssignMeleeHitboxScript();
            return;
        }
        meleeHitbox = transform.Find("MeleeHitbox")?.gameObject;
        if (meleeHitbox == null)
        {
            Debug.LogError($"MeleeHitbox không tìm thấy trong {transform.name}");
            return;
        }
        AssignMeleeHitboxScript();
    }

    private void AssignMeleeHitboxScript()
    {
        meleeHitboxScript = meleeHitbox.GetComponent<MeleeHitbox>();
        if (meleeHitboxScript == null)
            Debug.LogError($"MeleeHitbox script không tìm thấy trong {meleeHitbox.name}");
    }

    private void Start()
    {
        InitializeStateMachine();
        playerStateMachine.SetState(idleState);
    }

    private void Update()
    {
        HandleMovement();
        playerStateMachine.UpdateState();
        UpdateHurtLock();
    }

    private void UpdateHurtLock()
    {
        if (isHurtLocked)
        {
            hurtLockTimer -= Time.deltaTime;
            if (hurtLockTimer <= 0)
            {
                isHurtLocked = false;
            }
        }
    }

    private void HandleMovement()
    {
        // Chỉ khóa di chuyển trong thời gian bị đánh (hurtLockDuration)
        if (isHurtLocked) return;

        if (playerInput.Jump && onGround)
        {
            PlayerJump(Vector2.up);
        }
        else if (playerInput.BowAttack && onGround && !isAttacking)
        {
            StartAttack("Bow");
        }
        else if (playerInput.KnifeAttack && onGround && !isAttacking)
        {
            StartAttack("Knife");
        }
        else if (playerInput.MoveRight && !isAttacking)
        {
            PlayerMove(Vector2.right);
            if (!facingRight) RotatePlayer(false);
        }
        else if (playerInput.MoveLeft && !isAttacking)
        {
            PlayerMove(Vector2.left);
            if (facingRight) RotatePlayer(true);
        }
        else if (onGround && !isJumping && !isAttacking)
        {
            playerStateMachine.SetState(idleState);
        }
    }

    private void StartAttack(string attackType)
    {
        isAttacking = true;
        attackState.SetAttackType(attackType);
        playerStateMachine.SetState(attackState);
        Debug.Log($"Bắt đầu tấn công {attackType}");
    }

    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
        if (playerHealth.CurrentHealth > 0)
        {
            StartHurtAnimation();
        }
    }

    public void TakeDamageWithKnockback(int damage, Vector2 knockbackDirection = default)
    {
        TakeDamage(damage);
        if (playerHealth.CurrentHealth > 0 && knockbackDirection != Vector2.zero)
        {
            rb.velocity = new Vector2(knockbackDirection.x * knockbackForce, knockbackDirection.y * knockbackForce);
            isHurtLocked = true;
            hurtLockTimer = hurtLockDuration;
        }
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
        SetOnGround(false); // Sử dụng phương thức SetOnGround
        isJumping = true;
        playerStateMachine.SetState(jumpState);
    }

    private void PlayerMove(Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
        if (onGround)
        {
            playerStateMachine.SetState(runState);
        }
    }

    public void SetOnGround(bool value)
    {
        onGround = value;
    }

    public void SetIsJumping(bool value)
    {
        isJumping = value;
    }

    public void SetIsAttacking(bool value)
    {
        isAttacking = value;
    }

    private void RotatePlayer(bool flip)
    {
        facingRight = !flip;
        transform.rotation = Quaternion.Euler(0, flip ? 180 : 0, 0);
        AdjustMeleeHitbox(flip);
    }

    private void AdjustMeleeHitbox(bool flip)
    {
        if (meleeHitbox == null) return;
        meleeHitbox.transform.localRotation = Quaternion.Euler(0, flip ? 180 : 0, 0);
        Vector3 hitboxPos = meleeHitbox.transform.localPosition;
        hitboxPos.x = Mathf.Abs(hitboxPos.x) * (flip ? -1 : 1);
        meleeHitbox.transform.localPosition = hitboxPos;

        if (meleeHitbox.TryGetComponent(out BoxCollider2D boxCollider))
        {
            boxCollider.offset = new Vector2(Mathf.Abs(boxCollider.offset.x) * (flip ? -1 : 1), boxCollider.offset.y);
        }
    }

    public void ActivateHitbox() => meleeHitboxScript?.ActivateHitbox();
    public void DeactivateHitbox() => meleeHitboxScript?.DeactivateHitbox();

    public void StartHurtAnimation()
    {
        if (playerHealth.CurrentHealth > 0 && !(playerStateMachine.CurrentState is PlayerHurtState))
        {
            playerStateMachine.SetState(hurtState);
            Debug.Log("Player chuyển sang trạng thái Hurt");
        }
    }

    public void EndHurtAnimation()
    {
        if (onGround && playerStateMachine.CurrentState is PlayerHurtState)
        {
            playerStateMachine.SetState(idleState);
            Debug.Log("Player thoát trạng thái Hurt và chuyển về Idle");
        }
    }

    private void InitializeStateMachine()
    {
        playerStateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(animator);
        runState = new PlayerRunState(animator);
        attackState = new PlayerAttackState(animator, playerInput);
        jumpState = new PlayerJumpState(animator);
        hurtState = new PlayerHurtState(animator);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack") && !playerHealth.IsInvincible)
        {
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            TakeDamageWithKnockback(10, knockbackDirection);
        }
    }
}