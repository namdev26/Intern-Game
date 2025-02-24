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
        // Chọn ngẫu nhiên target ban đầu nếu chưa có giá trị trước đó
        if (target == Vector2.zero) // Chỉ chọn ngẫu nhiên lần đầu
        {
            target = Random.Range(0f, 1f) < 0.5f ? pointA : pointB;
        }
        Debug.Log($"Patrol từ A: {pointA} đến B: {pointB}, target hiện tại: {target}");
        minTimePatrol = 0f;
        timeSleep = Random.Range(2f, 5f); // Chọn thời gian tuần tra cố định khi vào trạng thái
        UpdateFacingDirection(target != pointB); // Quay mặt theo target ban đầu
    }

    public override void DoState()
    {
        minTimePatrol += Time.deltaTime;
        float speed = monster.patrolSpeed;

        // Chuyển sang trạng thái Idle nếu hết thời gian tuần tra
        if (minTimePatrol >= timeSleep)
        {
            // Chọn ngẫu nhiên target cho lần Patrol tiếp theo
            float randomTarget = Random.Range(0f, 1f);
            target = randomTarget < 0.5f ? pointA : pointB;
            Debug.Log($"Hết thời gian tuần tra, target mới cho lần sau: {target}");
            monster.ChangeState(monster.idleState);
            return; // Thoát để không tiếp tục di chuyển
        }
        // Di chuyển tới target hiện tại
        monster.transform.position = Vector2.MoveTowards(monster.transform.position, target, speed * Time.deltaTime);

        // Khi đến gần target, chuyển sang điểm còn lại và quay mặt
        if (Vector2.Distance(monster.transform.position, target) < 0.1f)
        {
            monster.ChangeState(monster.idleState);
            if (target == pointA)
            {
                target = pointB;
                UpdateFacingDirection(true); // Quay sang phải khi đi tới pointB
            }
            else
            {
                target = pointA;
                UpdateFacingDirection(false); // Quay sang trái khi đi tới pointA
            }
            Debug.Log($"Đã đến đích, target mới: {target}");
        }
    }

    public override void ExitState()
    {
        Debug.Log("Thoát trạng thái Patrol");
    }

    private void UpdateFacingDirection(bool flip)
    {
        monster.transform.rotation = Quaternion.Euler(0, flip ? 180 : 0, 0);
    }
}