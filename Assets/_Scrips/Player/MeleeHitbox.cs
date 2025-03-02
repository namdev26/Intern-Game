using UnityEngine;

public class MeleeHitbox : NamMonoBehaviour
{
    [SerializeField] private int damage = 30; // Sát thương cận chiến
    [SerializeField] private Collider2D meleeHitBoxCollider; // Collider của hitbox
    public int Damage => damage;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadColliderMeleeHitBox();
    }

    void LoadColliderMeleeHitBox()
    {
        if (meleeHitBoxCollider != null) return;
        this.meleeHitBoxCollider = GetComponent<Collider2D>();
        Debug.Log("Load ColliderMeleeHitBox");
    }

    public void ActivateHitbox() // Gọi từ Animation Event
    {
        if (meleeHitBoxCollider != null)
        {
            meleeHitBoxCollider.enabled = true;
        }
        else
        {
            Debug.LogWarning("meleeHitBoxCollider is null in ActivateHitbox!");
        }
    }

    public void DeactivateHitbox() // Gọi từ Animation Event
    {
        if (meleeHitBoxCollider != null)
        {
            meleeHitBoxCollider.enabled = false;
        }
        else
        {
            Debug.LogWarning("meleeHitBoxCollider is null in DeactivateHitbox!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GroundMonsterController monster = collision.GetComponent<GroundMonsterController>();
            if (monster != null)
            {
                monster.TakeDamage(damage);
            }
        }
    }
}