using IngameStateMachine;
using UnityEngine;
using User;

public class MetaGameState : MonoBehaviour, IState
{
    private StateMachine _stateMachine;
    private ICharacteristicsService _characteristicsService;
    private IMoneyService _moneyService;
    private MetaGameController _metaGameController;

    public MetaGameState(ICharacteristicsService characteristicsService, IMoneyService moneyService)
    {
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
        _metaGameController = FindObjectOfType<MetaGameController>();
        _metaGameController.Initialize(_characteristicsService, _moneyService);
    }

    public void OnExit()
    {
    }
}