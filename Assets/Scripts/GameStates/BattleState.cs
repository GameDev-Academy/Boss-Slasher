using ConfigurationProviders;
using IngameStateMachine;
using UniRx;
using User;

public class BattleState : IState
{
    private StateMachine _stateMachine;
    
    private BattleController _battleController;
    private ICharacteristicsService _characteristicsService;
    private IConfigurationProvider _configurationProvider;
    private ISceneLoadingService _sceneLoader;
    
    public BattleState(IConfigurationProvider configurationProvider, ICharacteristicsService characteristicService, ISceneLoadingService sceneLoader)
    {
        _configurationProvider = configurationProvider;
        _characteristicsService = characteristicService;
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
        _battleController.Initialize(_configurationProvider, _characteristicsService);
    }

    public void OnExit()
    {
    }
    
    public void Dispose()
    {
    }
}