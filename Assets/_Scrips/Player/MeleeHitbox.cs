using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    //[SerializeField] private int damage = 30; // Sát thương cận chiến
    //private bool isActive = false;
    [SerializeField] private Collider2D meleeHitBoxCollider; // Collider của hitbox cận chiến

    //public bool IsActive => isActive; // Getter để kiểm tra trạng thái hitbox
    //public int Damage => damage; // Getter để lấy sát thương

    public void ActivateHitbox()
    {
        meleeHitBoxCollider.enabled = true;
    }

    public void DeactivateHitbox()
    {
        meleeHitBoxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController monster = collision.GetComponent<MonsterController>();
        monster.TakeDamage(30);
    }
}