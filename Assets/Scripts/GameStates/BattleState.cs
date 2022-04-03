using ConfigurationProviders;
using IngameStateMachine;
using UniRx;

public class BattleState : IState
{
    private StateMachine _stateMachine;
    private BattleController _battleController;
    private IConfigurationProvider _configurationProvider;
    private ISceneLoadingService _sceneLoader;
    
    public BattleState(IConfigurationProvider configurationProvider, ISceneLoadingService sceneLoader)
    {
        _configurationProvider = configurationProvider;
        _sceneLoader = sceneLoader;
    }
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _sceneLoader
            .LoadSceneAndFind<BattleController>(SceneNames.BATTLE_SCENE)
            .Subscribe(OnSceneLoaded);
    }

    private void OnSceneLoaded(BattleController controller)
    {
        _battleController = controller;
        _battleController.Initialize(_configurationProvider);
    }

    public void OnExit()
    {
    }
    
    public void Dispose()
    {
    }
}