using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : NamMonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpSpeed = 11f;
    [SerializeField] public bool onGround;
    [SerializeField] private PlayerAnimation playerAnimation;

    private Vector2 VectorToRight = new Vector2(1, 0);
    private Vector2 VectorToLeft = new Vector2(-1, 0);
    private Vector2 VectorToUp = new Vector2(0, 1);

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRigidbody2D();
        this.LoadSpriteRenderer();
        this.LoadPlayerAnimation();
    }

    private void LoadRigidbody2D()
    {
        if (this.rigidBody2D != null) return;
        this.rigidBody2D = transform.GetComponent<Rigidbody2D>();
        Debug.LogWarning("Load Rigidbody2D");
    }

    private void LoadSpriteRenderer()
    {
        if (this.spriteRenderer != null) return;
        this.spriteRenderer = transform.GetComponent<SpriteRenderer>();
        Debug.LogWarning("Load SpriteRenderer");
    }

    private void LoadPlayerAnimation()
    {
        if (this.playerAnimation != null) return;
        this.playerAnimation = transform.GetComponentInChildren<PlayerAnimation>();
        Debug.LogWarning("Load PlayerAnimation");
    }

    void Update()
    {
        if (Input.GetKey("right"))
        {
            PlayerMove(VectorToRight);
            RotatePlayer(false);
        }
        else if (Input.GetKey("left"))
        {
            PlayerMove(VectorToLeft);
            RotatePlayer(true);
        }
        else
        {
            playerAnimation.PlayIdle(onGround);
        }

        if (Input.GetKeyDown("up") && onGround)
        {
            rigidBody2D.AddForce(VectorToUp * jumpSpeed, ForceMode2D.Impulse);
            StartCoroutine(playerAnimation.PlayJump());
        }
        if (Input.GetMouseButtonDown(0) && onGround)
        {
            StartCoroutine(playerAnimation.PlayAttackBow());
        }
        if (Input.GetMouseButtonDown(1) && onGround)
        {
            StartCoroutine(playerAnimation.PlayAttackKnife());
        }
    }

    void PlayerMove(Vector2 MoveVector)
    {
        if (playerAnimation.IsAttacking()) return; // Không di chuyển khi đang attack
        Vector2 NewMoveVector = new Vector2(MoveVector.x * moveSpeed, rigidBody2D.velocity.y);
        rigidBody2D.velocity = NewMoveVector;

        if (onGround)
        {
            playerAnimation.PlayRun();
        }
    }

    void RotatePlayer(bool flip)
    {
        spriteRenderer.flipX = flip;
    }
}