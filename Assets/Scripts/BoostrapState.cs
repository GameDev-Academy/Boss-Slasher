using IngameStateMachine;
using User;

public class BoostrapState : IState
{
    private StateMachine _stateMachine;

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