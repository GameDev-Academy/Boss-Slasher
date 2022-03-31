using ConfigurationProviders;
using IngameStateMachine;

public class BattleState : IState
{
    private StateMachine _stateMachine;
    private BattleController _battleController;
    private IConfigurationProvider _configurationProvider;
    private LoadingScene _loadingScene;
    
    public BattleState(IConfigurationProvider configurationProvider)
    {
        _configurationProvider = configurationProvider;
    }

    public void Dispose()
    {
    }

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _battleController.Initialize(_configurationProvider);
        _loadingScene.Initialize(GlobalConstants.GAME_SCENE);
    }

    public void OnExit()
    {
        _stateMachine.Enter<MetaGameState>();
    }
}