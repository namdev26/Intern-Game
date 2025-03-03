using UnityEngine;

public class GroundMonsterController : BaseMonsterController
{
    protected override void InitializeStates()
    {
        idleState = new MonsterIdleState(this, animator);
        patrolState = new MonsterPatrolState(this, animator);
        chaseState = new MonsterChaseState(this, animator);
        attackState = new MonsterAttackState(this, animator);
        dieState = new MonsterDieState(this, animator);
        hurtState = new MonsterHurtState(this, animator);
    }
}