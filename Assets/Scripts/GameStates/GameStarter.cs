using System;
using ConfigurationProviders;
using UnityEngine;
using IngameStateMachine;
using User;
using UserProgress;

public class GameStarter : MonoBehaviour
{
    [SerializeField] 
    private ConfigurationProvider _configurationProvider;

    private StateMachine _stateMachine;
    private SceneLoadingService _sceneLoader;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _sceneLoader = new SceneLoadingService(this);

        var characteristicsSettingsProvider = _configurationProvider.CharacteristicsSettings;
        //PlayerPrefs.DeleteAll();

        var userProfileService = new UserProfileService();
        var userProfile = userProfileService.CreateUserProfile();

        var moneyService = new MoneyService(userProfile.Money);
        var characteristicService = new CharacteristicsService(userProfile.CharacteristicsLevels, 
            characteristicsSettingsProvider,
            moneyService);
        
        var progressManager = new ProgressManager(characteristicService, moneyService);

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
}