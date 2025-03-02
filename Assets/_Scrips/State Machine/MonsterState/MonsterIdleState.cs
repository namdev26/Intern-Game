using UnityEngine;

public class MonsterIdleState : State
{
    private BaseMonsterController monster; // Thay đổi từ MonsterController sang BaseMonsterController
    private float idleTime;                // Thời gian đã đứng yên
    private float maxIdleTime;             // Thời gian tối đa đứng yên trước khi tuần tra (lấy từ MonsterData)

    public MonsterIdleState(BaseMonsterController monster, Animator animator) : base(animator)
    {
        this.monster = monster;
        // Lấy maxIdleTime từ MonsterData (đã có trong file bạn cung cấp)
        this.maxIdleTime = monster.MonsterData.maxIdleTime;
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

        if (monster.DistanceToPlayer() < monster.MonsterData.detectionRange) // Sử dụng data.detectionRange
        {
            monster.ChangeState(monster.ChaseState);
        }
        // Chuyển sang Patrol nếu đứng yên quá lâu
        else if (idleTime >= maxIdleTime)
        {
            monster.ChangeState(monster.PatrolState);
        }
    }

    public override void ExitState()
    {
        //Debug.Log("Thoát trạng thái Idle");
    }
}