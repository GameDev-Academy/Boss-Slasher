using ConfigurationsProviders;
using IngameStateMachine;

public class BoostrapState : IState
{
    private StateMachine _stateMachine;
    private UserProfile _userProfile;
    private IConfigurationProvider _configurationsProvider;

    public BoostrapState(UserProfile userProfile, IConfigurationProvider configurationsProvider)
    {
        _userProfile = userProfile;
        _configurationsProvider = configurationsProvider;
        configurationsProvider.Initialize();
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