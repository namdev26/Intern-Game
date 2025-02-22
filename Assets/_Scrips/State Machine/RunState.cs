using UnityEngine;

public class RunState : State
{
    public RunState(Animator animator) : base(animator) { }
    public override void EnterState()
    {
        animator.Play("Run");
    }

    public override void DoState() { }

    public override void ExitState() { }
}
