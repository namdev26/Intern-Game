using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IItem item = collision.gameObject.GetComponent<IItem>();
        if (item != null)
        {
            item.Collect();
        }
    }
}
