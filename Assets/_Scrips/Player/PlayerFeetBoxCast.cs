using UnityEngine;

public class PlayerFeetBoxCast : NamMonoBehaviour
{
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlayerController();
    }
    private void LoadPlayerController()
    {
        if (this.PlayerControllerScript != null) return;
        this.PlayerControllerScript = transform.GetComponentInParent<PlayerController>();
        Debug.LogWarning("Load PlayerController");
    }
    public PlayerController PlayerControllerScript;
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerControllerScript.onGround = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerControllerScript.onGround = false;
    }

}