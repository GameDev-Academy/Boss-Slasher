using ConfigurationProviders;
using UnityEngine;
using IngameStateMachine;
using User;

public class GameStarter : MonoBehaviour
{
    [SerializeField] 
    private ConfigurationProvider _configurationProvider;

    [SerializeField] 
    private MetaGameState _metaGameState;
    
    private StateMachine _stateMachine;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _configurationProvider.Initialize();
        
        //эти данные будем брать из сохраненных настроек (или с сервера)
        var userMoney = 9999;
        var moneyService = new MoneyService(userMoney);
        var characteristicService = new CharacteristicsService(_configurationProvider, moneyService);
        _metaGameState.Initialize(characteristicService, moneyService);

        var states = new IState[]
        {
            new BoostrapState(characteristicService),
            _metaGameState,
            //new MetaGameState(characteristicService, moneyService),
            new BattleState(characteristicService)
        };

        _stateMachine = new StateMachine(states);
        _stateMachine.Initialize();
        _stateMachine.Enter<BoostrapState>();
    }
}