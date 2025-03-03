using UnityEngine;

public class PlayerFeetBoxCast : NamMonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadPlayerController();
    }
    private void LoadPlayerController()
    {
        if (playerController != null) return;
        playerController = GetComponentInParent<PlayerController>();
        Debug.Log("PlayerFeetBoxCast: Load PlayerController");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerController.SetOnGround(true);
            playerController.SetIsJumping(false); // Đặt lại isJumping khi chạm đất
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerController.SetOnGround(false);
        playerController.SetIsJumping(true);// Đặt isJumping khi rời khỏi đất
    }
}
