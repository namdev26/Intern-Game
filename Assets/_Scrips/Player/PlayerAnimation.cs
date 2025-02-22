using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : NamMonoBehaviour
{
    [SerializeField] private Animator animator;
    private string currentAnimation = "";
    private bool isAttacking = false;
    public void PlayRun()
    {
        PlayAnimation("Run");
    }
    public IEnumerator PlayAttackBow() // Xử lý Attack logic
    {
        if (isAttacking) yield break; // Ngăn spam Attack
        isAttacking = true;
        PlayAnimation("Attack_Bow");
        yield return new WaitForSeconds(0.5f); // Đợi Attack chạy hết
        isAttacking = false;
    }
    public IEnumerator PlayAttackKnife() // Xử lý Attack logic
    {
        if (isAttacking) yield break; // Ngăn spam Attack
        isAttacking = true;
        PlayAnimation("Attack_Knife");
        yield return new WaitForSeconds(0.8f); // Đợi Attack chạy hết
        isAttacking = false;
    }
    public void PlayIdle(bool onGround)
    {
        if (onGround && !isAttacking) // Không Idle khi đang Attack
        {
            PlayAnimation("Idle");
        }
    }
    public IEnumerator PlayJump()
    {
        yield return new WaitForSeconds(0.05f);
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
    public bool IsAttacking()
    {
        return isAttacking;
    }
}

