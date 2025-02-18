using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
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
        if (Input.GetKey("left"))
        {
            this.PlayerMove(VectorToLeft);
            this.PlayerRotate(true);
        }
        if (Input.GetKeyDown("up") && onGround)
        {
            this.PlayerJump();
        }
    }
    void PlayerMove(Vector2 moveVector)
    {
        Vector2 newMoveVector = new Vector2(moveVector.x * moveSpeed, rigidBody2D.velocity.y);
        rigidBody2D.velocity = newMoveVector;
    }

    void PlayerJump()
    {
        rigidBody2D.AddForce(VectorToUp * jumpSpeed, ForceMode2D.Impulse);
    }
    void PlayerRotate(bool boolValue)
    {
        spriteRenderer.flipX = boolValue;
    }
}