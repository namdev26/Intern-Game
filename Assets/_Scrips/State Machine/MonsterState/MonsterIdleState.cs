using UnityEngine;

public class MonsterIdleState : State
{
    private MonsterController monster;
    private float idleTime;       // Thời gian đã đứng yên
    private float maxIdleTime = 2f; // Thời gian tối đa đứng yên trước khi tuần tra

    public MonsterIdleState(MonsterController monster, Animator animator) : base(animator)
    {
        this.monster = monster;
    }

    public override void EnterState()
    {
        //Debug.Log("Bắt đầu trạng thái Idle");
        animator.Play("Idle"); // Phát animation Idle
        idleTime = 0f;         // Reset thời gian đứng yên
    }

    public override void DoState()
    {
        // Tăng thời gian đứng yên
        idleTime += Time.deltaTime;

        if (monster.DistanceToPlayer() < monster.detectionRange)
        {
            monster.ChangeState(monster.chaseState);
        }
        // Chuyển sang Patrol nếu đứng yên quá lâu
        else if (idleTime >= maxIdleTime)
        {
            monster.ChangeState(monster.patrolState);
        }
    }

    public override void ExitState()
    {
        //Debug.Log("Thoát trạng thái Idle");
    }
}