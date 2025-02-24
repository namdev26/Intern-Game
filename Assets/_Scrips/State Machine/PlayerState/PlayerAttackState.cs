using UnityEngine;

public class PlayerAttackState : State
{
    private PlayerInput playerInput;

    public PlayerAttackState(Animator animator, PlayerInput playerInput) : base(animator)
    {
        this.playerInput = playerInput;
    }

    public override void EnterState()
    {
        if (playerInput.BowAttack)
        {
            animator.Play("BowAttack");
        }
        else if (playerInput.KnifeAttack)
        {
            animator.Play("KnifeAttack");
        }
    }

    public override void DoState()
    {
        // Không cần cập nhật gì đặc biệt
    }

    public override void ExitState() { }
}
