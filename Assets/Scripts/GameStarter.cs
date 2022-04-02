using ConfigurationProviders;
using UnityEngine;
using IngameStateMachine;
using UpgradeButtons;
using User;

public class GameStarter : MonoBehaviour
{
    [SerializeField] 
    private ConfigurationProvider _configurationProvider;

    [SerializeField] 
    private MetaGameController _metaGameController;
    
    private StateMachine _stateMachine;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _configurationProvider.Initialize();
        
        //эти данные будем брать из сохраненных настроек (или с сервера)
        var userMoney = 9999;
        var moneyService = new MoneyService(userMoney);
        var characteristicService = new CharacteristicsService(_configurationProvider, moneyService);

        var states = new IState[]
        {
            new BoostrapState(characteristicService),
            new MetaGameState(_metaGameController, characteristicService, moneyService),
            new BattleState(characteristicService)
        };

        _stateMachine = new StateMachine(states);
        _stateMachine.Initialize();
        _stateMachine.Enter<BoostrapState>();
    }
}