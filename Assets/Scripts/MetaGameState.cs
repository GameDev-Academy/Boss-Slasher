using ConfigurationProviders;
using IngameStateMachine;
using UnityEngine;
using User;

public class MetaGameState : IState
{
    private StateMachine _stateMachine;
    
    private readonly ICharacteristicsService _characteristicsService;
    private readonly IMoneyService _moneyService;
    private readonly IConfigurationProvider _configurationProvider;

    private MetaGameController _metaGameController;

    public MetaGameState(IConfigurationProvider configurationProvider, ICharacteristicsService characteristicsService, IMoneyService moneyService)
    {
        _configurationProvider = configurationProvider;
        _characteristicsService = characteristicsService;
        _moneyService = moneyService;
    }

    public void Dispose()
    {
    }

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    // TODO: Когда кидается какой-то ивент на старт игры - вызываем этот метод
    private void StartBattleHandler()
    {
        _stateMachine.Enter<BattleState>();
    }

    public void OnEnter()
    {
        // TODO: 
        // 1. Загружаем сцену
        // 2. Вызываем у MetaGameManager.Ininitialize и передаем туда userProfile
        _metaGameController = Object.FindObjectOfType<MetaGameController>();
        _metaGameController.Initialize(_configurationProvider, _characteristicsService, _moneyService);
    }

    public void OnExit()
    {
    }
}