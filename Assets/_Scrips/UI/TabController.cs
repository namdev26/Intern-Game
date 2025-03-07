using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [SerializeField] private Image[] tabImages;
    [SerializeField] private GameObject[] pages;
    void Start()
    {
        this.ActiveTab(0);
    }

    public void ActiveTab(int tabIndex)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabImages[i].color = Color.gray;
        }
        pages[tabIndex].SetActive(true);
        tabImages[tabIndex].color = Color.white;
    }
}
