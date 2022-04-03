using ConfigurationProviders;
using UnityEngine;
using IngameStateMachine;
using User;

public class GameStarter : MonoBehaviour
{
    [SerializeField] 
    private ConfigurationProvider _configurationProvider;

    private StateMachine _stateMachine;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _configurationProvider.Initialize();
        
        //эти данные будем брать из сохраненных настроек (или с сервера)
        var userMoney = 9999;
        var characteristicsSettingsProvider = _configurationProvider.CharacteristicsSettings;

        var moneyService = new MoneyService(userMoney);
        var characteristicService = new CharacteristicsService(characteristicsSettingsProvider, moneyService);

        var states = new IState[]
        {
            new BoostrapState(),
            new MetaGameState(_configurationProvider, characteristicService, moneyService),
            new BattleState()
        };

        _stateMachine = new StateMachine(states);
        _stateMachine.Initialize();
        _stateMachine.Enter<BoostrapState>();
    }
}