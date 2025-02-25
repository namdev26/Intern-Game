using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f; // Tốc độ bay của đạn (nhanh hơn mũi tên)
    public int damage = 20; // Sát thương
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed; // Bay theo hướng ban đầu
    }

    //void Update()
    //{
    //    Destroy(gameObject, 3f); // Hủy sau 3 giây nếu không trúng
    //}
}