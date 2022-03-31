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
        
        var userMoney = 9999;
        var moneyService = new MoneyService(_configurationProvider, userMoney);
        var characteristicService = new CharacteristicsService(_configurationProvider, moneyService);
        
        //var upgradeButtonsView = FindObjectOfType<UpgradeButtonsView>();
        //var userMoneyPresenter =  FindObjectOfType<UserMoneyPresenter>();
        
        //upgradeButtonsView.Initialize(characteristicService, moneyService);
        //userMoneyPresenter.Initialize(moneyService);

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