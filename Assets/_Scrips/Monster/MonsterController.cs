using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private State currentState;
    private Animator animator;
    public Transform player;

    // các thông số
    public float patrolSpeed = 2f; // tuần tra
    public float chaseSpeed = 4f; // đuổi x2
    public float detectionRange = 3f; // khoảng cách phát hiện
    public float attackRange = 1f; // đủ gần để attack
    public float patrolDistance = 15f; // khoang cách tuần tra
    public Vector2 startPos; // vị trí bắt đầu của quái

    // Các trạng thái
    public MonsterIdleState idleState;
    public MonsterPatrolState patrolState;
    public MonsterChaseState chaseState;
    public MonsterAttackState attackState;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPos = transform.position; // Lưu vị trí ban đầu cố định
        // khởi tạo
        idleState = new MonsterIdleState(this, animator);
        patrolState = new MonsterPatrolState(this, animator);
        chaseState = new MonsterChaseState(this, animator);
        attackState = new MonsterAttackState(this, animator);

        // bắt đầu ở idle
        ChangeState(idleState);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.DoState();
        }
    }

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState = newState;
        currentState.EnterState();
    }

    public float DistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.position);
    }
}