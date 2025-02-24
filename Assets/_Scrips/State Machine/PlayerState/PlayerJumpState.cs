using UnityEngine;

public class PlayerJumpState : State
{
    public PlayerJumpState(Animator animator) : base(animator) { }

    public override void EnterState()
    {
        animator.Play("Jump");
    }

    public override void DoState()
    {
        // Không cần cập nhật gì đặc biệt
    }

    public override void ExitState() { }
}
