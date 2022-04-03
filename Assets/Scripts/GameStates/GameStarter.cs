using System;
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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _sceneLoader = new SceneLoadingService(this);

        //эти данные будем брать из сохраненных настроек (или с сервера)
        var userMoney = 9999;
        var characteristicsSettingsProvider = _configurationProvider.CharacteristicsSettings;

        var moneyService = new MoneyService(userMoney);
        var characteristicService = new CharacteristicsService(characteristicsSettingsProvider, moneyService);

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