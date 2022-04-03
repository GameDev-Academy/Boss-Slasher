using ConfigurationProviders;
using IngameStateMachine;
using User;

public class BoostrapState : IState
{
    private StateMachine _stateMachine;
    private UserProfile _userProfile;
    private readonly IConfigurationProvider _configurationProvider;

    public BoostrapState(IUserProfileService userProfileService, IConfigurationProvider configurationProvider)
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