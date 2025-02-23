using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D Rigidbody2D;

    void Start()
    {
        Rigidbody2D.velocity = transform.right * speed;
    }
}
