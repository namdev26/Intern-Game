
using UnityEngine;

public class PlayerHPBarUI : MonoBehaviour
{
    public RectTransform RectTransform;
    int HPBarWithMax = 135;
    int PlayerHPMax = PlayerHealth.MaxHealth;

    public void UpdateHPPlayer(int PlayerCurrentHP)
    {
        float newHPBarWith = PlayerCurrentHP * HPBarWithMax / PlayerHPMax;
        RectTransform.sizeDelta = new Vector2(newHPBarWith, RectTransform.sizeDelta.y);
    }
}
