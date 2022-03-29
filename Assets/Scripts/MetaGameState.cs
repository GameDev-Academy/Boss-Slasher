using IngameStateMachine;

public class MetaGameState : IState
{
    private StateMachine _stateMachine;
    private UserProfile _userProfile;
    private ConfigurationProvider _configurationProvider;

    public MetaGameState(UserProfile userProfile, ConfigurationProvider configurationProvider)
    {
        _userProfile = userProfile;
        _configurationProvider = configurationProvider;
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