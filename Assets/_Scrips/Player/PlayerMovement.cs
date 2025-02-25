//using UnityEngine;

//public class PlayerMovement : MonoBehaviour
//{
//    [SerializeField] private float moveSpeed = 5f;
//    [SerializeField] private float jumpForce = 5f;
//    private Rigidbody2D rb;
//    private bool onGround;
//    private bool isJumping;

//    public bool IsOnGround => onGround;
//    public bool IsJumping => isJumping;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }

//    public void Move(Vector2 direction)
//    {
//        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
//    }

//    public void Jump()
//    {
//        if (!onGround || isJumping) return;
//        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
//        onGround = false;
//        isJumping = true;
//    }

//    // Gọi từ Physics hoặc Animation để kiểm tra mặt đất (cần thêm logic phát hiện ground)
//    public void SetOnGround(bool value)
//    {
//        onGround = value;
//        if (onGround) isJumping = false;
//    }
//}