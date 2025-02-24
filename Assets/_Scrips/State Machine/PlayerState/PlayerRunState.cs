using UnityEngine;

public class PlayerRunState : State
{
    public PlayerRunState(Animator animator) : base(animator) { }
    public override void EnterState()
    {
        animator.Play("Run");
    }

    public override void DoState() { }

    public override void ExitState() { }
}
