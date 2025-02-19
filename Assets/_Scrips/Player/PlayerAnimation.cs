using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : NamMonoBehaviour
{
    [SerializeField] private Animator animator;
    private string currentAnimation = "";

    public PlayerAnimation(Animator animator)
    {
        this.animator = animator;
    }
    public void PlayRun()
    {
        PlayAnimation("Run");
    }
    public void PlayIdle(bool onGround)
    {
        if (onGround)
        {
            PlayAnimation("Idle");
        }
    }
    public IEnumerator PlayJump()
    {
        yield return new WaitForSeconds(0.1f);
        PlayAnimation("Jump");
    }
    private void PlayAnimation(string animationName)
    {
        if (currentAnimation != animationName)
        {
            currentAnimation = animationName;
            animator.Play(currentAnimation);
            Debug.Log("Playing: " + currentAnimation);
        }
    }
}

