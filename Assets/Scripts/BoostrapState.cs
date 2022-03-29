using IngameStateMachine;

public class BoostrapState : IState
{
    private StateMachine _stateMachine;
    private UserProfile _userProfile;
    private ConfigurationProvider _configurationProvider;

    public BoostrapState(UserProfile userProfile, ConfigurationProvider configurationProvider)
    {
        _userProfile = userProfile;
        _configurationProvider = configurationProvider;
        configurationProvider.Initialize();
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
        CreateFakeUserProfile();
        _stateMachine.Enter<MetaGameState>();
    }

    public void OnExit()
    {
    }

    private void CreateFakeUserProfile()
    {
        _userProfile.Initialize(_configurationProvider.CharacteristicsSettingsProvider);
    }
}