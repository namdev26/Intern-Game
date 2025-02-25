using UnityEngine;

public class MonsterAttackHitbox : MonoBehaviour
{
    private MonsterController monsterController;
    [SerializeField] private Collider2D collider2DHitBox; // Tham chiếu đến MonsterController của quái

    void Start()
    {
        // Tìm MonsterController từ parent (quái cha)
        monsterController = GetComponentInParent<MonsterController>();
        if (monsterController == null)
        {
            Debug.LogError("MonsterController not found in parent of AttackHitbox!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (monsterController == null) return;

        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(10); // Gây 10 sát thương cho Player (có thể điều chỉnh)
                Debug.Log($"{monsterController.name} hit Player with attack, causing 10 damage!");
            }
        }
    }

    public void setAtiveCollider()
    {
        collider2DHitBox.enabled = true;
    }
    public void setDeativeCollider()
    {
        collider2DHitBox.enabled = false;
    }
}