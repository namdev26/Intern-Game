using UnityEngine;

public class Arrow : NamMonoBehaviour
{
    [SerializeField] private float speedShoot = 30f;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    void Start()
    {
        _rigidbody2D.velocity = transform.right * speedShoot;
    }

}

