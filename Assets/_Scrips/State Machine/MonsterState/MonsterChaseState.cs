using UnityEngine;

public class MonsterChaseState : State
{
    private MonsterController monster;

    public MonsterChaseState(MonsterController monster, Animator animator) : base(animator)
    {
        this.monster = monster;
    }

    public override void EnterState()
    {
        Debug.Log("Bắt đầu trạng thái Chase");
        animator.Play("Chase");
    }

    public override void DoState()
    {
        // Đuổi theo người chơi
        Vector2 direction = (monster.player.position - monster.transform.position).normalized;
        monster.transform.position += (Vector3)direction * monster.chaseSpeed * Time.deltaTime;

        // Chuyển sang Attack nếu đủ gần
        if (monster.DistanceToPlayer() < monster.attackRange)
        {
            monster.ChangeState(monster.attackState);
        }
        // Chuyển sang Idle nếu mất dấu người chơi
        else if (monster.DistanceToPlayer() > monster.detectionRange)
        {
            monster.ChangeState(monster.idleState);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Thoát trạng thái Chase");
    }
}