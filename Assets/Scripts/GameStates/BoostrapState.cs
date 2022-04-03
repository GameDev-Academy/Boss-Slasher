using ConfigurationProviders;
using IngameStateMachine;

public class BoostrapState : IState
{
    private StateMachine _stateMachine;
    private readonly IConfigurationProvider _configurationProvider;

    public BoostrapState(IConfigurationProvider configurationProvider)
    {
        _configurationProvider = configurationProvider;
    }
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    public void OnEnter()
    {
        _configurationProvider.Initialize();
        _stateMachine.Enter<MetaGameState>();
    }
    
    public void OnExit()
    {
    }
    
    public void Dispose()
    {
    }
}