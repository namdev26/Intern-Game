using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject hotBarPanel;
    void Start()
    {
        menuCanvas.SetActive(false);
        //hotBarPanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hotBarPanel.SetActive(!hotBarPanel.activeSelf);
        }
    }
}
