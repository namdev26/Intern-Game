using System.Collections;
using UnityEngine;

public class PlayerController : NamMonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpSpeed = 11f;
    [SerializeField] public bool onGround;
    [SerializeField] private Animator playerAnimator;
    public string currentAnimation = "";
    private Vector2 VectorToRight = new Vector2(1, 0);
    private Vector2 VectorToLeft = new Vector2(-1, 0);
    private Vector2 VectorToUp = new Vector2(0, 1);

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlayerAnimation();
        this.Rigidbody2D();
        this.LoadSpriteRenderer();
    }
    private void LoadPlayerAnimation()
    {
        if (this.playerAnimator != null) return;
        this.playerAnimator = transform.GetComponent<Animator>();
        Debug.LogWarning("Load PlayerAnimator");
    }
    private void Rigidbody2D()
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
            AnimationStop();
        }

        if (Input.GetKeyDown("up") && onGround)
        {
            rigidBody2D.AddForce(VectorToUp * jumpSpeed, ForceMode2D.Impulse);
            StartCoroutine(AnimationJump());
        }
    }
    void PlayerMove(Vector2 MoveVector)
    {
        Vector2 NewMoveVector = new Vector2(MoveVector.x * moveSpeed, rigidBody2D.velocity.y);
        rigidBody2D.velocity = NewMoveVector;

        if (onGround)
        {
            PlayingAnimation("Run");
        }

    }
    void RotatePlayer(bool Bool_Value)
    {
        spriteRenderer.flipX = Bool_Value;
    }
    void AnimationStop()
    {
        if (onGround)
        {
            PlayingAnimation("Idle");
        }
    }
    IEnumerator AnimationJump()
    {
        yield return new WaitForSeconds(0.1f);
        PlayingAnimation("Jump");
    }
    void PlayingAnimation(string AnimationName)
    {
        if (currentAnimation != AnimationName)
        {
            currentAnimation = AnimationName;
            playerAnimator.Play(currentAnimation);
            Debug.Log("Playing: " + currentAnimation);
        }
    }
}