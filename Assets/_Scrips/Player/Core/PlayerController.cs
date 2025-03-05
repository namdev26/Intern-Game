using UnityEngine;

public class PlayerController : NamMonoBehaviour // Giả sử NamMonoBehaviour là base class
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject meleeHitbox;
    [SerializeField] private MeleeHitbox meleeHitboxScript;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerHPBarUI playerHPBarUI;

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
    [SerializeField] private float hurtLockDuration = 0.5f;
    private bool isHurtLocked;
    private float hurtLockTimer;
    [SerializeField] private float knockbackForce = 5f;

    private void Start()
    {
        InitializeStateMachine();
        playerStateMachine.SetState(idleState);
        playerHPBarUI.UpdateHPPlayer(playerHealth.CurrentHealth);
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
        if (isHurtLocked) return;

        if (playerInput.Jump && onGround)
        {
            if (isAttacking) InterruptAttack();
            PlayerJump(Vector2.up);
        }
        else if (playerInput.BowAttack && onGround)
        {
            if (!isAttacking)
            {
                StartAttack("Bow");
            }
            else
            {
                InterruptAttack();
                StartAttack("Bow");
            }
        }
        else if (playerInput.KnifeAttack && onGround)
        {
            if (!isAttacking)
            {
                StartAttack("Knife");
            }
            else
            {
                InterruptAttack();
                StartAttack("Knife");
            }
        }
        else if (playerInput.MoveRight)
        {
            if (isAttacking) InterruptAttack();
            PlayerMove(Vector2.right);
            if (!facingRight) RotatePlayer(false);
        }
        else if (playerInput.MoveLeft)
        {
            if (isAttacking) InterruptAttack();
            PlayerMove(Vector2.left);
            if (facingRight) RotatePlayer(true);
        }
        else if (onGround && !isJumping && !isAttacking)
        {
            playerStateMachine.SetState(idleState);
        }
    }

    #region Player Actions
    private void StartAttack(string attackType)
    {
        isAttacking = true;
        attackState.SetAttackType(attackType);
        playerStateMachine.SetState(attackState);
        ActivateHitbox(); // Kích hoạt hitbox khi bắt đầu tấn công
        Debug.Log($"Bắt đầu tấn công {attackType}");
    }

    private void InterruptAttack()
    {
        if (!isAttacking) return;

        isAttacking = false;
        DeactivateHitbox(); // Tắt hitbox ngay lập tức
        animator.Play("Idle"); // Chuyển về Idle ngay lập tức
        playerStateMachine.SetState(idleState); // Chuyển state về Idle
        Debug.Log("Tấn công bị ngắt quãng");
    }

    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
        playerHPBarUI.UpdateHPPlayer(playerHealth.CurrentHealth);
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
        DeactivateHitbox();
        Debug.Log("Đã reset trạng thái tấn công");
        if (onGround && !isJumping)
        {
            playerStateMachine.SetState(idleState);
        }
    }

    private void PlayerJump(Vector2 direction)
    {
        if (!onGround || isJumping) return;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        SetOnGround(false);
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
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack") && !playerHealth.IsInvincible)
        {
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            TakeDamageWithKnockback(10, knockbackDirection);
        }
    }
}