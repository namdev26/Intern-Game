using UnityEngine;

public class IdleState : State
{
    public IdleState(Animator animator) : base(animator) { }

    public override void EnterState()
    {
        animator.Play("Idle");
    }

    public override void DoState()
    {
        // Không cần cập nhật gì đặc biệt
    }

    public override void ExitState() { }
}
