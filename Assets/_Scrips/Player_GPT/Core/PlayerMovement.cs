using UnityEngine;

public class PlayerMovement : NamMonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerModel playerModel;
    [SerializeField] private Rigidbody2D rb;
    private PlayerStateMachine playerStateMachine;

    private IdleState idleState;
    private RunState runState;
    private AttackState attackState;
    private JumpState jumpState;

    private Vector2 VectorToRight = new Vector2(1, 0);
    private Vector2 VectorToLeft = new Vector2(-1, 0);
    private Vector2 VectorToUp = new Vector2(0, 1);

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlayerInput();
        this.LoadPlayerModel();
        this.LoadRigidbody2D();
    }

    protected virtual void LoadPlayerInput()
    {
        if (this.playerInput != null) return;
        this.playerInput = transform.parent.Find("PlayerInput").GetComponent<PlayerInput>();
        Debug.Log(transform.name + " Load PlayerInput", gameObject);
    }

    protected virtual void LoadPlayerModel()
    {
        if (this.playerModel != null) return;
        this.playerModel = transform.parent.Find("PlayerModel").GetComponent<PlayerModel>();
        Debug.Log(transform.name + " Load PlayerModel", gameObject);
    }

    protected virtual void LoadRigidbody2D()
    {
        if (this.rb != null) return;
        this.rb = GetComponentInParent<Rigidbody2D>();
        Debug.Log(transform.name + " Load Rigidbody2D", gameObject);
    }

    private void Start()
    {
        playerStateMachine = new PlayerStateMachine();
        idleState = new IdleState(playerModel.Animator);
        runState = new RunState(playerModel.Animator);
        attackState = new AttackState(playerModel.Animator, playerInput);
        jumpState = new JumpState(playerModel.Animator);

        playerStateMachine.SetState(idleState);
    }

    private void Update()
    {
        HandleMovement();
        playerStateMachine.UpdateState();
    }

    private void HandleMovement()
    {
        if (playerInput.MoveRight)
        {
            PlayerMove(VectorToRight);
            RotatePlayer(false);
        }
        else if (playerInput.MoveLeft)
        {
            PlayerMove(VectorToLeft);
            RotatePlayer(true);
        }
        else
        {
            playerStateMachine.SetState(idleState);
        }
    }

    private void PlayerMove(Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
        playerStateMachine.SetState(runState);
    }

    private void RotatePlayer(bool flip)
    {
        transform.parent.rotation = Quaternion.Euler(0, flip ? 180 : 0, 0);
    }
}
