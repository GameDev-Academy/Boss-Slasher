using ConfigurationProviders;
using Events;
using IngameStateMachine;
using User;
using UniRx;

public class MetaGameState : IState
{
    private StateMachine _stateMachine;
    
    private readonly ICharacteristicsService _characteristicsService;
    private readonly IMoneyService _moneyService;
    private readonly IConfigurationProvider _configurationProvider;
    private readonly ISceneLoadingService _sceneLoader;
    
    private MetaGameController _metaGameController;
    private CompositeDisposable _subscription;

    public MetaGameState(
        IConfigurationProvider configurationProvider, 
        ISceneLoadingService sceneLoader, 
        ICharacteristicsService characteristicsService, 
        IMoneyService moneyService)
    {
        _sceneLoader = sceneLoader;
        _configurationProvider = configurationProvider;
        _characteristicsService = characteristicsService;
        _moneyService = moneyService;
    }
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    public void OnEnter()
    {
        _sceneLoader
            .LoadSceneAndFind<MetaGameController>(SceneNames.METAGAME_SCENE)
            .Subscribe(OnSceneLoaded);
        
        _subscription = new CompositeDisposable
        {
            EventStreams.UserInterface.Subscribe<OpenShopEvent>(OpenShopHandler),
            EventStreams.UserInterface.Subscribe<StartBattleEvent>(StartBattleHandler)
        };
        
        //TODO: Show loading screen
    }

    private void OnSceneLoaded(MetaGameController metaGameController)
    {
        _metaGameController = metaGameController;
        _metaGameController.Initialize(_configurationProvider, _characteristicsService, _moneyService);
    }

    private void StartBattleHandler(StartBattleEvent eventData)
    {
        _stateMachine.Enter<BattleState>();
    }
    
    private void OpenShopHandler(OpenShopEvent eventData)
    {
        _stateMachine.Enter<ShoppingState>();
    }
    
    public void OnExit()
    {
        _subscription.Dispose();
    }
    
    public void Dispose()
    {
    }
}