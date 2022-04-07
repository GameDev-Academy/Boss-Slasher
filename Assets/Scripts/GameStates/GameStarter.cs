using ConfigurationProviders;
using UnityEngine;
using IngameStateMachine;
using User;

public class GameStarter : MonoBehaviour
{
    [SerializeField] 
    private ConfigurationProvider _configurationProvider;

    private StateMachine _stateMachine;
    private SceneLoadingService _sceneLoader;
    private UserProfileService _userProfileService;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _sceneLoader = new SceneLoadingService(this);
        _userProfileService = new UserProfileService();
        
        var userProfile = _userProfileService.LoadOrCreateDefaultProfile();
        
        var moneyService = new MoneyService(userProfile.Money);
        
        var characteristicsSettingsProvider = _configurationProvider.CharacteristicsSettings;
        var characteristicService = new CharacteristicsService(userProfile.CharacteristicsLevels, 
            characteristicsSettingsProvider,
            moneyService);
        
        _userProfileService.Initialize(characteristicService, moneyService);

        var states = new IState[]
        {
            new BoostrapState(_configurationProvider),
            new MetaGameState(_configurationProvider, _sceneLoader, characteristicService, moneyService),
            new ShoppingState(_configurationProvider, _sceneLoader),
            new BattleState(_configurationProvider, _sceneLoader)
        };

        _stateMachine = new StateMachine(states);
        _stateMachine.Initialize();
        _stateMachine.Enter<BoostrapState>();
    }

    public void OnDestroy()
    {
        _userProfileService.Dispose();
    }
}