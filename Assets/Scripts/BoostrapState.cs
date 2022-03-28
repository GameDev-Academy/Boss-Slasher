using IngameStateMachine;

public class BoostrapState : IState
{
    private StateMachine _stateMachine;

    public BoostrapState(UserProfile userProfile)
    {
    }

    public void Dispose()
    {
    }

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _stateMachine.Enter<MetaGameState>();
    }

    public void OnExit()
    {
    }
}