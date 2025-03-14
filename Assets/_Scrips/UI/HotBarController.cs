using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotBarController : MonoBehaviour
{
    public GameObject hotBarPanel;
    public GameObject slotPrefab;
    public int slotCount = 6;
    private ItemDictionary itemDictionary;

    private Key[] hotBarKeys;

    private void Awake()
    {
        itemDictionary = FindObjectOfType<ItemDictionary>();
        hotBarKeys = new Key[slotCount];
        for (int i = 0; i < slotCount; i++)
        {
            hotBarKeys[i] = i < 5 ? (Key)(int)(Key.Digit1 + i) : Key.Digit0;
        }
    }

    private void Update()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (Keyboard.current[hotBarKeys[i]].wasPressedThisFrame)
            {
                UseItemInSlot(i);
            }
        }
    }

    void UseItemInSlot(int slotIndex)
    {
        Slot slot = hotBarPanel.transform.GetChild(slotIndex).GetComponent<Slot>();
        if (slot.currentItem != null)
        {
            Item item = slot.currentItem.GetComponent<Item>();
            item.UseItem();
        }
    }

    public List<InventorySaveData> GetHotBarItem()
    {
        List<InventorySaveData> hotBarSaveDatas = new List<InventorySaveData>();
        foreach (Transform slotTransform in hotBarPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                hotBarSaveDatas.Add(new InventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() });
            }
        }
        return hotBarSaveDatas;
    }

    public void SetHotBarItem(List<InventorySaveData> hotBarSaveData)
    {
        // clear inventory panell
        if (itemDictionary == null) Debug.LogError("itemDictionary chưa được khởi tạo!");
        foreach (Transform child in hotBarPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, hotBarPanel.transform);
        }

        foreach (InventorySaveData data in hotBarSaveData)
        {
            if (data.slotIndex < slotCount)
            {
                Slot slot = hotBarPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                GameObject itemPrefabs = itemDictionary.GetItemPrefab(data.itemID);
                if (itemPrefabs != null)
                {
                    GameObject item = Instantiate(itemPrefabs, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                }
            }
        }

    }
}
