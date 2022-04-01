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
    private MetaGameController metaGameController;
    
    private StateMachine _stateMachine;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _configurationProvider.Initialize();
        
        var userMoney = 9999;
        var moneyService = new MoneyService(_configurationProvider, userMoney);
        var characteristicService = new CharacteristicsService(_configurationProvider, moneyService);

        var states = new IState[]
        {
            new BoostrapState(characteristicService),
            new MetaGameState(metaGameController, characteristicService, moneyService),
            new BattleState(characteristicService)
        };

        _stateMachine = new StateMachine(states);
        _stateMachine.Initialize();
        _stateMachine.Enter<BoostrapState>();
    }
}