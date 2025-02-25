using UnityEngine;

public class PlayerStateMachine
{
    private State currentState;

    public State CurrentState => currentState; // Thêm property công khai để truy cập currentState

    public void SetState(State newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public void UpdateState()
    {
        currentState?.DoState();
    }
}