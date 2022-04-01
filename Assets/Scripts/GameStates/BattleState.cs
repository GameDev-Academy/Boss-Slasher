using ConfigurationProviders;
using IngameStateMachine;

public class BattleState : IState
{
    private StateMachine _stateMachine;
    private BattleController _battleController;
    private IConfigurationProvider _configurationProvider;
    private SceneLoader _sceneLoader;
    
    public BattleState(IConfigurationProvider configurationProvider, SceneLoader sceneLoader, BattleController battleController)
    {
        _configurationProvider = configurationProvider;
        _sceneLoader = sceneLoader;
        _battleController = battleController;
    }
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _battleController.Initialize(_configurationProvider);
        _sceneLoader.Load(SceneNames.BATTLE_SCENE);
    }

    public void OnExit()
    {
    }
    
    public void Dispose()
    {
    }
}