using IngameStateMachine;

public class BoostrapState : IState
{
    private StateMachine _stateMachine;
    private ICharacteristicsService _characteristicsService;

    public BoostrapState(ICharacteristicsService characteristicsService)
    {
        _characteristicsService = characteristicsService;
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