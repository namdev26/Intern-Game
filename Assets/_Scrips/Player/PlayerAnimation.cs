using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void IdleAnimation()
    {
        _animator.Play("Idle");
    }
    public void RunAnimation()
    {
        _animator.Play("Run");
    }
    public void JumpAnimation()
    {
        _animator.Play("Jump");
    }
    public void AttackAnimation()
    {
        _animator.Play("Attack");
    }
    public void DeathAnimation()
    {
        _animator.Play("Death");
    }
    public void HurtAnimation()
    {
        _animator.Play("Hurt");
    }
    public void TurnAnimation()
    {
        _animator.Play("Turn");
    }

}
