using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    [SerializeField] private GameObject meleeHitbox; // Tham chiếu đến hitbox cận chiến
    private MeleeHitbox meleeHitboxScript; // Tham chiếu đến script MeleeHitbox

    void Start()
    {
        // Load MeleeHitbox
        meleeHitbox = transform.Find("MeleeHitbox")?.gameObject;
        if (meleeHitbox == null)
        {
            Debug.LogError("MeleeHitbox không tìm thấy trong " + transform.name);
        }
        else
        {
            meleeHitboxScript = meleeHitbox.GetComponent<MeleeHitbox>();
            if (meleeHitboxScript == null)
            {
                Debug.LogError("MeleeHitbox script không tìm thấy trong " + meleeHitbox.name);
            }
            else
            {
                Debug.Log("MeleeHitbox được tải thành công trong PlayerShooting");
            }
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Bắn đạn!");
    }

    private void DeactivateMeleeHitbox()
    {
        if (meleeHitboxScript != null)
        {
            meleeHitboxScript.DeactivateHitbox();
            Debug.Log("PlayerShooting gọi DeactivateHitbox");
        }
        else
        {
            Debug.LogWarning("meleeHitboxScript là null trong PlayerShooting");
        }
    }
}