//using UnityEngine;

//public class PlayerAttack : MonoBehaviour
//{
//    [SerializeField] private GameObject meleeHitbox; // Hitbox cận chiến
//    [SerializeField] private Transform shootPoint; // Điểm bắn đạn (nếu có)
//    [SerializeField] private GameObject bulletPrefab; // Prefab đạn (nếu có)
//    private MeleeHitbox meleeHitboxScript; // Tham chiếu đến script MeleeHitbox
//    private bool isAttacking = false;
//    private string attackType; // "Bow" hoặc "Knife"
//    private Animator animator; // Thêm Animator để chơi animation

//    void Start()
//    {
//        animator = GetComponent<Animator>();
//        meleeHitbox = transform.Find("MeleeHitbox")?.gameObject;
//        if (meleeHitbox == null) Debug.LogWarning("MeleeHitbox không tìm thấy trong " + transform.name);
//        meleeHitboxScript = meleeHitbox?.GetComponent<MeleeHitbox>();
//        if (meleeHitboxScript == null && meleeHitbox != null) Debug.LogWarning("MeleeHitbox script không tìm thấy trong " + meleeHitbox.name);

//        // Load ShootPoint nếu có
//        if (shootPoint == null)
//        {
//            shootPoint = transform.Find("ShootPoint");
//            if (shootPoint == null) Debug.LogWarning("ShootPoint không tìm thấy trong " + transform.name);
//        }
//    }

//    public void SetAttackType(string type)
//    {
//        attackType = type;
//        if (type == "Bow")
//        {
//            Shoot();
//            animator.Play("BowAttack"); // Kích hoạt animation BowAttack
//        }
//        else if (type == "Knife")
//        {
//            MeleeAttack();
//            animator.Play("KnifeAttack"); // Kích hoạt animation KnifeAttack
//        }
//    }

//    public bool IsAttacking => isAttacking;

//    public void ResetAttack()
//    {
//        isAttacking = false;
//    }

//    private void Shoot()
//    {
//        if (shootPoint != null && bulletPrefab != null)
//        {
//            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
//            Debug.Log("Bắn đạn!");
//        }
//    }

//    private void MeleeAttack()
//    {
//        if (meleeHitboxScript != null && !isAttacking)
//        {
//            isAttacking = true;
//            meleeHitboxScript.ActivateHitbox();
//            Debug.Log("Tấn công cận chiến!");
//            Invoke("DeactivateHitbox", 1f); // Tắt hitbox sau 1 giây
//        }
//    }

//    private void DeactivateHitbox()
//    {
//        if (meleeHitboxScript != null)
//        {
//            meleeHitboxScript.DeactivateHitbox();
//            isAttacking = false;
//        }
//    }

//    public void RotateHitbox(bool flip)
//    {
//        if (meleeHitbox != null)
//        {
//            meleeHitbox.transform.localRotation = Quaternion.Euler(0, flip ? 180 : 0, 0);
//            Vector3 hitboxPos = meleeHitbox.transform.localPosition;
//            hitboxPos.x = Mathf.Abs(hitboxPos.x) * (flip ? -1 : 1);
//            meleeHitbox.transform.localPosition = hitboxPos;

//            BoxCollider2D boxCollider = meleeHitbox.GetComponent<BoxCollider2D>();
//            if (boxCollider != null)
//            {
//                boxCollider.offset = new Vector2(Mathf.Abs(boxCollider.offset.x) * (flip ? -1 : 1), boxCollider.offset.y);
//            }
//        }
//    }
//}