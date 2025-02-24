using UnityEngine;

public class MonsterPatrolState : State
{
    private MonsterController monster;
    private Vector2 pointA; // Điểm bắt đầu (startPos)
    private Vector2 pointB; // Điểm đích
    private Vector2 target; // Đang di chuyển tới đích hiện tại
    private float minTimePatrol; // Thời gian đã trôi qua
    private float timeSleep; // Thời gian tuần tra cố định cho mỗi lần vào trạng thái

    public MonsterPatrolState(MonsterController monster, Animator animator) : base(animator)
    {
        this.monster = monster;
    }

    public override void EnterState()
    {
        Debug.Log("Bắt đầu trạng thái Patrol");
        animator.Play("Patrol");
        pointA = monster.startPos; // Điểm A là vị trí khởi tạo cố định
        pointB = pointA + Vector2.right * monster.patrolDistance; // Điểm B cách A một khoảng
        if (target == Vector2.zero) // Chỉ chọn ngẫu nhiên lần đầu
        {
            target = Random.Range(0f, 1f) < 0.5f ? pointA : pointB;
        }
        Debug.Log($"Patrol từ A: {pointA} đến B: {pointB}, target hiện tại: {target}");
        minTimePatrol = 0f;
        timeSleep = Random.Range(2f, 5f);
        monster.UpdateFacingDirection(target); // Quay mặt theo target ban đầu
    }

    public override void DoState()
    {
        minTimePatrol += Time.deltaTime;
        float speed = monster.patrolSpeed;

        if (monster.player != null && Vector2.Distance(monster.transform.position, monster.player.position) < monster.detectionRange)
        {
            Debug.Log("Phát hiện người chơi, chuyển sang trạng thái Chase");
            monster.ChangeState(monster.chaseState);
            return;
        }

        if (minTimePatrol >= timeSleep)
        {
            float randomTarget = Random.Range(0f, 1f);
            target = randomTarget < 0.5f ? pointA : pointB;
            Debug.Log($"Hết thời gian tuần tra, target mới cho lần sau: {target}");
            monster.ChangeState(monster.idleState);
            return;
        }

        monster.transform.position = Vector2.MoveTowards(monster.transform.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(monster.transform.position, target) < 0.1f)
        {
            monster.ChangeState(monster.idleState);
            if (target == pointA)
            {
                target = pointB;
            }
            else
            {
                target = pointA;
            }
            monster.UpdateFacingDirection(target); // Quay mặt theo target mới
            Debug.Log($"Đã đến đích, target mới: {target}");
        }
    }

    public override void ExitState()
    {
        Debug.Log("Thoát trạng thái Patrol");
    }
}