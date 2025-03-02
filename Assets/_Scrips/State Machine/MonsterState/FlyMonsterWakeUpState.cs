using UnityEngine;

public class FlyMonsterWakeUpState : State
{
    BaseMonsterController monster;
    public FlyMonsterWakeUpState(BaseMonsterController monster, Animator animator) : base(animator)
    {
        this.monster = monster;
    }
    public override void EnterState()
    {
        animator.Play("WakeUp");
    }

    public override void DoState()
    {
        monster.ChangeState(monster.IdleState);
    }

    public override void ExitState()
    {
        // do nothing
    }
}
