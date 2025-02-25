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
    private PlayerStateMachine playerStateMachine;

    private PlayerIdleState idleState;
    private PlayerRunState runState;
    private PlayerAttackState attackState;
    private PlayerJumpState jumpState;

    private Vector2 VectorToRight = Vector2.right;
    private Vector2 VectorToLeft = Vector2.left;
    private Vector2 VectorToUp = Vector2.up;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadPlayerInput();
        LoadAnimator();
        LoadRigidbody2D();
    }

    protected virtual void LoadPlayerInput()
    {
        if (playerInput != null) return;
        playerInput = GetComponentInChildren<PlayerInput>();
        Debug.Log($"{transform.name} Load PlayerInput", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (animator != null) return;
        animator = GetComponentInChildren<Animator>();
        Debug.Log($"{transform.name} Load Animator", gameObject);
    }

    protected virtual void LoadRigidbody2D()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log($"{transform.name} Load Rigidbody2D", gameObject);
    }

    private void Start()
    {
        playerStateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(animator);
        runState = new PlayerRunState(animator);
        attackState = new PlayerAttackState(animator, playerInput);
        jumpState = new PlayerJumpState(animator);

        playerStateMachine.SetState(idleState);
    }

    private void Update()
    {
        HandleMovement();
        playerStateMachine.UpdateState();
    }

    private void HandleMovement()
    {
        // Không thoát sớm khi isAttacking, cho phép ngắt trạng thái Attack

        if (playerInput.Jump && onGround)
        {
            if (isAttacking)
            {
                isAttacking = false; // Ngắt trạng thái Attack nếu nhảy
                Debug.Log("Ngắt trạng thái Attack để nhảy");
            }
            PlayerJump(VectorToUp);
        }
        else if ((playerInput.BowAttack || playerInput.KnifeAttack) && onGround && !isAttacking)
        {
            // Chỉ vào trạng thái Attack nếu chưa tấn công
            isAttacking = true;
            playerStateMachine.SetState(attackState);
        }
        else if (playerInput.MoveRight)
        {
            if (isAttacking)
            {
                isAttacking = false; // Ngắt trạng thái Attack nếu di chuyển
                Debug.Log("Ngắt trạng thái Attack để chạy phải");
            }
            PlayerMove(VectorToRight);
            RotatePlayer(false);
        }
        else if (playerInput.MoveLeft)
        {
            if (isAttacking)
            {
                isAttacking = false; // Ngắt trạng thái Attack nếu di chuyển
                Debug.Log("Ngắt trạng thái Attack để chạy trái");
            }
            PlayerMove(VectorToLeft);
            RotatePlayer(true);
        }
        else if (onGround && !isJumping && !isAttacking)
        {
            playerStateMachine.SetState(idleState);
        }
    }

    public void ResetAttackState()
    {
        isAttacking = false;
        Debug.Log("Trạng thái tấn công kết thúc tự nhiên (Animation Event)");
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
    }

    private void PlayerMove(Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
        if (onGround)
        {
            playerStateMachine.SetState(runState);
        }
    }
}