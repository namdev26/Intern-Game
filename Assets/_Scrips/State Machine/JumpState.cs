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
        // Kh�ng c?n c?p nh?t g� ??c bi?t
    }

    public override void ExitState() { }
}
