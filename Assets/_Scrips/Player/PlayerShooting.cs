using UnityEngine;

public class PlayerShooting : NamMonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject meleeHitbox;
    private MeleeHitbox meleeHitboxScript;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadFirePoint();
        //this.LoadBulletPrefab();
        this.LoadMeleeHitbox();
    }

    void LoadFirePoint()
    {
        if (firePoint != null) return;
        this.firePoint = transform.Find("ShootPoint");
    }
    void LoadMeleeHitbox()
    {
        if (meleeHitbox != null) return;
        this.meleeHitbox = GetComponentInChildren<MeleeHitbox>().gameObject;
    }
    public void Shoot() // bắn cung
    {
        if (firePoint == null || bulletPrefab == null)
        {
            Debug.LogWarning($"{gameObject.name}: Không thể bắn vì FirePoint hoặc BulletPrefab bị thiếu!");
            return;
        }

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Debug.Log($"{gameObject.name}: Bắn đạn!");
    }

    public void DeactivateMeleeHitbox() // bật khoảng cách attack gần
    {
        if (meleeHitboxScript != null)
        {
            meleeHitboxScript.DeactivateHitbox();
            Debug.Log($"{gameObject.name}: Gọi DeactivateHitbox");
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: meleeHitboxScript là null!");
        }
    }
}
