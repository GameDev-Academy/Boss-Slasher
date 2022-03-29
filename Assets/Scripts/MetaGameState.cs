using ConfigurationsProviders;
using IngameStateMachine;

public class MetaGameState : IState
{
    private StateMachine _stateMachine;
    private UserProfile _userProfile;
    private ConfigurationsProvider _configurationsProvider;

    public MetaGameState(UserProfile userProfile, ConfigurationsProvider configurationsProvider)
    {
        _userProfile = userProfile;
        _configurationsProvider = configurationsProvider;
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