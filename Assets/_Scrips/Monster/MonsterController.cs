using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private State currentState;
    private Animator animator;
    public Transform player;

    // Các thông số
    [SerializeField] public float patrolSpeed = 2f; // Tốc độ tuần tra
    [SerializeField] public float chaseSpeed = 4f; // Tốc độ đuổi (nhanh gấp đôi)
    [SerializeField] public float detectionRange = 3f; // Khoảng cách phát hiện người chơi
    [SerializeField] public float attackRange = 1f; // Khoảng cách đủ gần để tấn công
    [SerializeField] public float patrolDistance = 15f; // Khoảng cách tuần tra
    [SerializeField] public Vector2 startPos; // Vị trí bắt đầu của quái

    // Các trạng thái
    public MonsterIdleState idleState;
    public MonsterPatrolState patrolState;
    public MonsterChaseState chaseState;
    public MonsterAttackState attackState;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogWarning("Player with tag 'Player' not found in the scene!");
        }

        startPos = transform.position; // Lưu vị trí ban đầu cố định

        // Khởi tạo các trạng thái
        idleState = new MonsterIdleState(this, animator);
        patrolState = new MonsterPatrolState(this, animator);
        chaseState = new MonsterChaseState(this, animator);
        attackState = new MonsterAttackState(this, animator);

        // Bắt đầu ở trạng thái Idle
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
        if (currentState != null)
        {
            currentState.EnterState();
        }
        else
        {
            Debug.LogError("Attempted to change to a null state!");
        }
    }

    public float DistanceToPlayer()
    {
        if (player == null)
        {
            return Mathf.Infinity; // Trả về vô cực nếu không có người chơi
        }
        return Vector2.Distance(transform.position, player.position);
    }

    // Hàm quay mặt theo một vị trí đích
    public void UpdateFacingDirection(Vector2 targetPosition)
    {
        bool flip = targetPosition.x > transform.position.x;
        transform.rotation = Quaternion.Euler(0, flip ? 0 : 180, 0);
        Debug.Log($"Quay mặt: flip = {flip}, rotation.y = {transform.rotation.eulerAngles.y}");
    }

    // Hàm quay mặt theo người chơi (tùy chọn nếu cần dùng trong Chase/Attack)
    public void UpdateFacingDirectionToPlayer()
    {
        if (player != null)
        {
            bool flip = player.position.x > transform.position.x;
            transform.rotation = Quaternion.Euler(0, flip ? 180 : 0, 0);
            Debug.Log($"Quay mặt về người chơi: flip = {flip}, rotation.y = {transform.rotation.eulerAngles.y}");
        }
    }
}