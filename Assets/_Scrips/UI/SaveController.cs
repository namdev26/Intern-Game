
using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private InventoryController inventoryController;
    private HotBarController hotBarController;


    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath + "saveData.json");
        inventoryController = FindObjectOfType<InventoryController>();
        hotBarController = FindObjectOfType<HotBarController>();
        //Debug.Log("Đường dẫn lưu file: " + Application.persistentDataPath);
        LoadGame();
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            inventorySaveData = inventoryController.GetInventoryItem(),
            hotBarSaveData = hotBarController.GetHotBarItem()
        };
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));

    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            inventoryController.SetInventoryItem(saveData.inventorySaveData);
            hotBarController.SetHotBarItem(saveData.hotBarSaveData);
        }
        else
        {
            SaveGame();
        }
    }
}
