using ConfigurationProviders;
using IngameStateMachine;
using User;

public class BoostrapState : IState
{
    private StateMachine _stateMachine;
    private UserProfile _userProfile;
    private IConfigurationProvider _configurationProvider;
    private IUserProfileService _userProfileService;
    private LoadingScene _loadingScene;

    public BoostrapState(IUserProfileService userProfileService, IConfigurationProvider configurationProvider)
    {
        _configurationProvider = configurationProvider;
        _userProfileService = userProfileService;
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
        _configurationProvider.Initialize();
        _userProfileService.CreateDefaultUserProfile(_configurationProvider);
        
        _loadingScene.Initialize(GlobalConstants.METAGAME_SCENE);
    }

    public void OnExit()
    {
    }
}