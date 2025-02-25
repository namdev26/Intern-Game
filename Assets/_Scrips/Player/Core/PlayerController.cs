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
    private GameObject meleeHitbox; // Biến private cho hitbox
    [SerializeField] private MeleeHitbox meleeHitboxScript; // Tham chiếu đến script MeleeHitbox

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
        LoadMeleeHitbox();
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
        else
        {
            Debug.Log("MeleeHitbox được tải thành công trong PlayerController");
        }
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
        if (playerInput.Jump && onGround)
        {
            if (isAttacking)
            {
                isAttacking = false;
            }
            PlayerJump(VectorToUp);
        }
        else if (playerInput.BowAttack && onGround)
        {
            isAttacking = true;
            attackState.SetAttackType("Bow");
            playerStateMachine.SetState(attackState);
            Debug.Log("Bắt đầu tấn công Bow");
        }
        else if (playerInput.KnifeAttack && onGround)
        {
            isAttacking = true;
            attackState.SetAttackType("Knife");
            playerStateMachine.SetState(attackState);
            Debug.Log("Bắt đầu tấn công Knife - Kiểm tra Animation Event");
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
        Debug.Log("Nhảy!");
    }

    private void RotatePlayer(bool flip)
    {
        transform.rotation = Quaternion.Euler(0, flip ? 180 : 0, 0);
        if (meleeHitbox != null)
        {
            // Xoay MeleeHitbox theo hướng của Player (localRotation)
            meleeHitbox.transform.localRotation = Quaternion.Euler(0, flip ? 180 : 0, 0);
            Debug.Log($"Đã xoay MeleeHitbox: localRotation.y = {meleeHitbox.transform.localRotation.eulerAngles.y}");

            // Điều chỉnh vị trí hitbox theo hướng
            Vector3 hitboxPos = meleeHitbox.transform.localPosition;
            hitboxPos.x = Mathf.Abs(hitboxPos.x) * (flip ? -1 : 1);
            meleeHitbox.transform.localPosition = hitboxPos;

            // Điều chỉnh BoxCollider2D offset để khớp với hướng
            BoxCollider2D boxCollider = meleeHitbox.GetComponent<BoxCollider2D>();
            if (boxCollider != null)
            {
                boxCollider.offset = new Vector2(Mathf.Abs(boxCollider.offset.x) * (flip ? -1 : 1), boxCollider.offset.y);
                Debug.Log($"Đã điều chỉnh BoxCollider2D offset: {boxCollider.offset}");
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
}