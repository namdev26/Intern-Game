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

    //protected override void Start()
    //{
    //    base.Start();
    //}

    //protected override void Update()
    //{
    //    base.Update();
    //}

    //// Quay mặt theo người chơi (tương tự MonsterController)
    //public void UpdateFacingDirectionToPlayer()
    //{
    //    if (player != null)
    //    {
    //        UpdateFacingDirection(player.position);
    //    }
    //}
}