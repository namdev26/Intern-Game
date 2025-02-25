using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    [SerializeField] private int damage = 30; // Sát thương cận chiến
    private bool isActive = false;

    public bool IsActive => isActive; // Getter để kiểm tra trạng thái hitbox
    public int Damage => damage; // Getter để lấy sát thương

    public void ActivateHitbox()
    {
        isActive = true;
        Debug.Log("Hitbox cận chiến bật - Trạng thái: " + isActive);
    }

    public void DeactivateHitbox()
    {
        isActive = false;
        Debug.Log("Hitbox cận chiến tắt - Trạng thái: " + isActive);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("OnTriggerStay2D được gọi - isActive: " + isActive + ", Tag: " + collision.tag);
        if (isActive && collision.CompareTag("Enemy"))
        {
            MonsterController monster = collision.GetComponent<MonsterController>();
            if (monster != null)
            {
                monster.TakeDamage(damage);
                Debug.Log($"Gây {damage} sát thương cận chiến cho quái vật {collision.name}");
            }
            else
            {
                Debug.LogWarning("Không tìm thấy MonsterController trên " + collision.name);
            }
        }
    }
}