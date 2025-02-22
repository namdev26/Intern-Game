using UnityEngine;

public class JumpState : State
{
    public JumpState(Animator animator) : base(animator) { }

    public override void EnterState()
    {
        animator.Play("Jump");
    }

    public override void DoState()
    {
        // Không c?n c?p nh?t gì ??c bi?t
    }

    public override void ExitState() { }
}
