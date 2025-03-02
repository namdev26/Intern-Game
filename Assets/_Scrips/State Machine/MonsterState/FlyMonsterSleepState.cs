using UnityEngine;

public class FlyMonsterSleepState : State
{
    private BaseMonsterController monster;

    public FlyMonsterSleepState(BaseMonsterController monster, Animator animator) : base(animator)
    {
        this.monster = monster;
    }

    public override void EnterState()
    {
        animator.Play("Sleep");
    }
    public override void DoState()
    {
        if (monster.DistanceToPlayer() <= monster.MonsterData.wakeUpRange)
        {
            monster.ChangeState(monster.FlyWakeUpState);
        }
    }

    public override void ExitState()
    {
        // Do nothing
    }
}
