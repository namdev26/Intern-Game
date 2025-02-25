//using UnityEngine;
//using System.Collections;

//public class PlayerHealth : MonoBehaviour
//{
//    [SerializeField] private int maxHealth = 100;
//    private int currentHealth;
//    private float invincibilityDuration = 2f;
//    private float damageCooldown = 1f;
//    private float lastDamageTime;
//    private bool isInvincible;
//    private SpriteRenderer spriteRenderer;

//    public int CurrentHealth => currentHealth;
//    public bool IsInvincible => isInvincible;

//    void Start()
//    {
//        currentHealth = maxHealth;
//        lastDamageTime = -damageCooldown;
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        if (spriteRenderer == null) Debug.LogWarning("SpriteRenderer không tìm thấy trong " + transform.name);
//    }

//    public void TakeDamage(int damage)
//    {
//        if (Time.time - lastDamageTime >= damageCooldown && !isInvincible)
//        {
//            currentHealth -= damage;
//            Debug.Log($"Player nhận {damage} sát thương. Máu còn: {currentHealth}");
//            lastDamageTime = Time.time;
//            StartInvincibility();

//            if (currentHealth <= 0)
//            {
//                Die();
//            }
//        }
//        else
//        {
//            Debug.Log("Player đang trong cooldown hoặc bất tử, không nhận thêm sát thương!");
//        }
//    }

//    private void Die()
//    {
//        Debug.Log("Player đã chết!");
//        gameObject.SetActive(false);
//    }

//    private void StartInvincibility()
//    {
//        isInvincible = true;
//        StartCoroutine(BlinkWhileInvincible());
//        Invoke("EndInvincibility", invincibilityDuration);
//    }

//    private void EndInvincibility()
//    {
//        isInvincible = false;
//        if (spriteRenderer != null)
//        {
//            spriteRenderer.color = Color.white;
//        }
//        Debug.Log("Player hết thời gian bất tử");
//    }

//    private IEnumerator BlinkWhileInvincible()
//    {
//        if (spriteRenderer == null) yield break;

//        float blinkInterval = 0.1f;
//        while (isInvincible)
//        {
//            spriteRenderer.color = spriteRenderer.color == Color.white ? Color.red : Color.white;
//            yield return new WaitForSeconds(blinkInterval);
//        }
//    }
//}