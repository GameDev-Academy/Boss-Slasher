using ConfigurationProviders;
using UnityEngine;
using IngameStateMachine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] 
    private ConfigurationProvider _configurationProvider;
    
    private StateMachine _stateMachine;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        var money = 9999;
        var moneyService = new MoneyService(_configurationProvider, money);
        var characteristicService = new CharacteristicsService(_configurationProvider, moneyService);

        var states = new IState[]
        {
            new BoostrapState(characteristicService),
            new MetaGameState(_configurationProvider, characteristicService, moneyService),
            new BattleState(characteristicService)
        };

        _stateMachine = new StateMachine(states);
        _stateMachine.Initialize();
        _stateMachine.Enter<BoostrapState>();
    }
}