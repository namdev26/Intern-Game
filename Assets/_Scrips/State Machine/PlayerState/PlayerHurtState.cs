using UnityEngine;

public class PlayerHurtState : State
{
    private float hurtDuration = 0.5f; // Th?i gian tr?ng th�i Hurt k�o d�i
    private float hurtTimer;

    public PlayerHurtState(Animator animator) : base(animator)
    {
    }

    public override void EnterState()
    {
        animator.Play("Hurt"); // Ch?y animation Hurt
        hurtTimer = hurtDuration;
        Debug.Log("B?t ??u tr?ng th�i Hurt");
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
        Debug.Log("Tho�t tr?ng th�i Hurt");
    }
}