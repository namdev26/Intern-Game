using UnityEngine;

public class PlayerIdleState : State
{
    public PlayerIdleState(Animator animator) : base(animator) { }

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
