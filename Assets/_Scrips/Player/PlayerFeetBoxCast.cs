using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeetBoxCast : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private void OnTriggerStay2D(Collider2D collision)
    {
        playerController.onGround = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerController.onGround = false;
    }
}
