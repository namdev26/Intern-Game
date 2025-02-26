using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Máu tối đa
    private int currentHealth; // Máu hiện tại

    // Thêm biến để theo dõi thời gian cooldown nhận sát thương
    [SerializeField] private float damageCooldown = 1f; // Thời gian chờ 1 giây giữa các lần nhận sát thương
    private float lastDamageTime; // Thời điểm nhận sát thương gần nhất

    // Thêm biến cho thời gian bất tử
    private bool isInvincible = false; // Trạng thái bất tử
    //private float invincibilityDuration = 2f; // Thời gian bất tử 2 giây (comment nếu không cần)
    //private float invincibilityTimer; // Đếm ngược thời gian bất tử (comment nếu không cần)

    public event System.Action OnDeath; // Event thông báo khi chết

    private void Start()
    {
        currentHealth = maxHealth; // Khởi tạo máu
        lastDamageTime = -damageCooldown; // Khởi tạo để có thể nhận sát thương ngay lập tức ban đầu
    }

    public void TakeDamage(int damage)
    {
        // Kiểm tra cooldown và trạng thái bất tử trước khi nhận sát thương
        if (Time.time - lastDamageTime >= damageCooldown && !isInvincible)
        {
            currentHealth -= damage;
            Debug.Log($"Player nhận {damage} sát thương. Máu còn: {currentHealth}");
            lastDamageTime = Time.time; // Cập nhật thời gian nhận sát thương gần nhất

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        else
        {
            Debug.Log("Player đang trong cooldown hoặc bất tử, không nhận thêm sát thương!");
        }
    }

    private void Die()
    {
        Debug.Log("Player đã chết!");
        OnDeath?.Invoke();
        gameObject.SetActive(false); // Tạm thời vô hiệu hóa Player
    }

    // Getter cho currentHealth (nếu cần truy cập từ các class khác)
    public int CurrentHealth => currentHealth;

    // Getter/Setter cho các thuộc tính khác (nếu cần)
    public bool IsInvincible
    {
        get => isInvincible;
        set => isInvincible = value;
    }

}