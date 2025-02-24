using UnityEngine;

public class IdleState : State
{
    private MonsterController monsterController;
    private float idleTime;       // Thời gian đã đứng yên
    private float maxIdleTime = 3f; // Thời gian tối đa đứng yên trước khi tuần tra

    public IdleState(MonsterController monsterController, Animator animator) : base(animator)
    {
        this.monsterController = monsterController;
    }

    public override void EnterState()
    {
        Debug.Log("Bắt đầu trạng thái Idle");
        animator.Play("Idle"); // Phát animation Idle
        idleTime = 0f;         // Reset thời gian đứng yên
    }

    public override void DoState()
    {
        // Tăng thời gian đứng yên
        idleTime += Time.deltaTime;

        // Chuyển sang Chase nếu phát hiện người chơi
        if (monsterController.DistanceToPlayer() < monsterController.detectionRange)
        {
            monsterController.ChangeState(monsterController.chaseState);
        }
        // Chuyển sang Patrol nếu đứng yên quá lâu
        else if (idleTime >= maxIdleTime)
        {
            monsterController.ChangeState(monsterController.patrolState);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Thoát trạng thái Idle");
    }
}