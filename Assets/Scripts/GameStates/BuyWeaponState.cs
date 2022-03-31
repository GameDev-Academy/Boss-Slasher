using ConfigurationProviders;
using IngameStateMachine;

public class BuyWeaponState : IState
{
    private StateMachine _stateMachine;
    private IConfigurationProvider _configurationProvider;
    public BuyWeaponState(IConfigurationProvider configurationProvider)
    {
        _configurationProvider = configurationProvider;
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
    }

    public void OnExit()
    {
    }
}