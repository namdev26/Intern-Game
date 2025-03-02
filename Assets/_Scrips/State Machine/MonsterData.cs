using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Monster/MonsterData")]
public class MonsterData : ScriptableObject
{
    public string monsterName = "Monster";
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float detectionRange = 3f;
    public float attackRange = 1f;
    public float patrolDistance = 15f;
    public float maxIdleTime = 2f; // Thời gian đứng yên tối đa
    public int maxHealth = 10;
    public float wakeUpRange = 2f;
    public bool moveHorizontallyOnly = true; // True: chỉ di chuyển ngang (quái đất), False: di chuyển cả X/Y (quái bay)
}