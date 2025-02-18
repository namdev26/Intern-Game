using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _moveSpeed = 5f;
    private Vector2 VectorToRight = new Vector2(1, 0);
    private Vector2 VectorToLeft = new Vector2(-1, 0);
    void Update()
    {
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            this.PlayerMove(VectorToRight);
            this.PlayerRotate(false);
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            this.PlayerMove(VectorToLeft);
            this.PlayerRotate(true);
        }
    }

    void PlayerMove(Vector2 vector)
    {
        _rigidbody2D.velocity = vector * _moveSpeed;
    }

    void PlayerRotate(bool boolValue)
    {
        _spriteRenderer.flipX = boolValue;
    }
}