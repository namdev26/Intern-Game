using UnityEngine;

public class FlyMonsterController : BaseMonsterController
{

    protected override void InitializeStates()
    {
        idleState = new MonsterIdleState(this, animator);
        patrolState = new MonsterPatrolState(this, animator);
        chaseState = new MonsterChaseState(this, animator);
        attackState = new MonsterAttackState(this, animator);
        dieState = new MonsterDieState(this, animator);
        hurtState = new MonsterHurtState(this, animator);
        flySleepState = new FlyMonsterSleepState(this, animator);
        flyWakeUpState = new FlyMonsterWakeUpState(this, animator);
    }

    protected override void Start()
    {
        base.Start();
        ChangeState(flySleepState);
    }

    protected override void Update()
    {
        base.Update();
    }

    // Quay m?t theo ng??i ch?i (t??ng t? MonsterController)
    public void UpdateFacingDirectionToPlayer()
    {
        if (player != null)
        {
            UpdateFacingDirection(player.position);
        }
    }
}