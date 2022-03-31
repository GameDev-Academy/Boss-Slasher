using ConfigurationProviders;
using IngameStateMachine;

public class MetaGameState : IState
{
    private StateMachine _stateMachine;
    private IConfigurationProvider _configurationProvider;
    private ICharacteristicsService _characteristicsService;
    private IMoneyService _moneyService;

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
    }

    public void OnExit()
    {
    }
}