using UnityEngine;

public class PlayerAttackState : State
{
    //private Animator animator;
    private PlayerInput playerInput;
    private string attackType;

    public PlayerAttackState(Animator animator, PlayerInput playerInput) : base(animator)
    {
        this.animator = animator;
        this.playerInput = playerInput;
    }

    public void SetAttackType(string type)
    {
        attackType = type;
    }

    public override void EnterState()
    {
        Debug.Log("Bắt đầu trạng thái Attack: " + attackType);
        if (attackType == "Bow")
        {
            animator.Play("BowAttack");
        }
        else if (attackType == "Knife")
        {
            animator.Play("KnifeAttack");
        }
    }

    public override void DoState()
    {
        // Không cần xử lý, Animation Event sẽ gọi ResetAttackState
    }

    public override void ExitState()
    {
        Debug.Log("Thoát trạng thái Attack");
    }
}