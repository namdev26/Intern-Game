using UnityEngine;

public class PlayerHurtState : State
{
    private float hurtDuration = 0.5f; // Th?i gian tr?ng thái Hurt kéo dài
    private float hurtTimer;

    public PlayerHurtState(Animator animator) : base(animator)
    {
    }

    public override void EnterState()
    {
        animator.Play("Hurt"); // Ch?y animation Hurt
        hurtTimer = hurtDuration;
        Debug.Log("B?t ??u tr?ng thái Hurt");
    }

    public override void DoState()
    {
        hurtTimer -= Time.deltaTime;
        if (hurtTimer <= 0)
        {
            PlayerController player = animator.GetComponentInParent<PlayerController>();
            if (player != null && player.OnGround)
            {
                player.PlayerStateMachine.SetState(new PlayerIdleState(animator));
            }
        }
    }

    public override void ExitState()
    {
        Debug.Log("Thoát tr?ng thái Hurt");
    }
}