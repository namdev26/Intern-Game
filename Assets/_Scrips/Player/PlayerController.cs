using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpSpeed = 16f;
    [SerializeField] private Animator playerAnimation;
    [SerializeField] public bool onGround;
    private Vector2 VectorToRight = new Vector2(1, 0);
    private Vector2 VectorToLeft = new Vector2(-1, 0);
    private Vector2 VectorToUp = new Vector2(0, 1);
    void Update()
    {
        if (Input.GetKey("right"))
        {
            this.PlayerMove(VectorToRight);
            this.PlayerRotate(false);
        }
        else if (Input.GetKey("left"))
        {
            this.PlayerMove(VectorToLeft);
            this.PlayerRotate(true);
        }
        else
        {
            this.AnimationStop();
        }
        if (Input.GetKeyDown("up") && onGround)
        {
            rigidBody2D.AddForce(VectorToUp * jumpSpeed, ForceMode2D.Impulse);
            StartCoroutine(AnimationJump());
        }

    }
    void PlayerMove(Vector2 moveVector)
    {
        Vector2 newMoveVector = new Vector2(moveVector.x * moveSpeed, rigidBody2D.velocity.y);
        rigidBody2D.velocity = newMoveVector;
        if (onGround)
        {
            playerAnimation.Play("Run");
        }
    }
    void PlayerRotate(bool boolValue)
    {
        spriteRenderer.flipX = boolValue;
    }
    IEnumerator AnimationJump()
    {
        yield return new WaitForSeconds(0.1f);
        playerAnimation.Play("Jump");
    }
    void AnimationStop()
    {
        if (onGround)
        {
            playerAnimation.Play("Idle");
        }
    }
}