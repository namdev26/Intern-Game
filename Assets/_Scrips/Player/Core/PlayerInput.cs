using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool MoveLeft { get; private set; }
    public bool MoveRight { get; private set; }
    public bool Jump { get; private set; }
    public bool BowAttack { get; private set; } // T?n công t?m xa (cung)
    public bool KnifeAttack { get; private set; }  // T?n công c?n chi?n (dao)
    public bool IsMoving => MoveLeft || MoveRight;

    private void Update()
    {
        MoveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        MoveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        Jump = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);

        BowAttack = Input.GetMouseButtonDown(0); // Chu?t trái = Cung
        KnifeAttack = Input.GetMouseButtonDown(1);  // Chu?t ph?i = Dao
    }
}
