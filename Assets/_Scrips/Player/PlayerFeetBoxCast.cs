using UnityEngine;

public class PlayerFeetBoxCast : MonoBehaviour
{
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