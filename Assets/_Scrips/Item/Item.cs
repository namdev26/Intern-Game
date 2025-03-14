using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int ID;
    public string itemName;

    public virtual void UseItem()
    {
        Debug.Log("Using " + itemName);
    }
    public virtual void Pickup()
    {
        Sprite itemIcon = GetComponent<Image>().sprite;

        if (ItemPickupUIController.Instance != null)
        {
            ItemPickupUIController.Instance.ShowItemPickup(itemName, itemIcon);
        }
    }
}
