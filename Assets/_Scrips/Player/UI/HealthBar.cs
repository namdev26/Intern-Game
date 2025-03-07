using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider Slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;
    public Camera uiCamera; // Thêm tham chiếu đến camera UI (nếu dùng Screen Space - Camera)

    void Start()
    {
        if (Slider == null)
        {
            Debug.LogError("Slider not assigned in HealthBar script!");
            return;
        }
        // Đảm bảo Slider luôn hiển thị ban đầu (có thể điều chỉnh logic sau)
        Slider.gameObject.SetActive(true);
    }

    public void SetHealth(float health, float maxHealth)
    {
        Debug.Log("Setting health: " + health + ", maxHealth: " + maxHealth + ", normalizedValue: " + (health / maxHealth));
        Slider.gameObject.SetActive(health < maxHealth);
        Slider.value = health;
        Slider.maxValue = maxHealth;

        if (Slider.fillRect != null)
        {
            Image fillImage = Slider.fillRect.GetComponent<Image>();
            if (fillImage != null)
            {
                fillImage.color = Color.Lerp(Low, High, Slider.normalizedValue);
                Debug.Log("Fill color set to: " + fillImage.color);
            }
            else
            {
                Debug.LogError("Image component missing on Slider.fillRect!");
            }
        }
        else
        {
            Debug.LogError("Slider.fillRect is null!");
        }
    }

    void Update()
    {
        if (uiCamera == null)
        {
            Debug.LogWarning("UI Camera not assigned or Camera.main is null. Using Camera.main as fallback.");
            uiCamera = Camera.main;
        }

        if (uiCamera != null && Slider != null)
        {
            // Chuyển đổi vị trí từ World Space sang Screen Space
            Slider.transform.position = uiCamera.WorldToScreenPoint(transform.parent.position + Offset);
        }
        else
        {
            Debug.LogError("Camera or Slider is null in HealthBar Update!");
        }
    }
}