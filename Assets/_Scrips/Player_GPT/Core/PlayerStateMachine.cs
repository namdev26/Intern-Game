public class PlayerStateMachine
{
    private State currentState;

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
