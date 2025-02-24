using UnityEngine;

public abstract class State
{
    protected Animator animator;

    public State(Animator animator)
    {
        this.animator = animator;
    }

    public abstract void EnterState();
    public abstract void DoState();
    public abstract void ExitState();

}
