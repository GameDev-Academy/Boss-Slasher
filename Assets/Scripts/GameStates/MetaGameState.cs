using ConfigurationProviders;
using User;
using UniRx;
using Unity.VisualScripting;
using IState = IngameStateMachine.IState;
using StateMachine = IngameStateMachine.StateMachine;

public class MetaGameState : IState
{
    private StateMachine _stateMachine;
    
    private readonly ICharacteristicsService _characteristicsService;
    private readonly IMoneyService _moneyService;
    private readonly IConfigurationProvider _configurationProvider;
    private readonly ISceneLoadingService _sceneLoader;
    
    private MetaGameController _metaGameController;

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

        MetaGameController.ButtonShopPressed += OnButtonShopPressed;
        //TODO: Show loading screen
        //TODO: При нажатии кнопки Начала игры сюда прилетает ивент или сделать колбек  на метод StartBattleHandler
    }

    private void OnSceneLoaded(MetaGameController metaGameController)
    {
        _metaGameController = metaGameController;
        _metaGameController.Initialize(_configurationProvider, _characteristicsService, _moneyService);
    }

    private void StartBattleHandler()
    {
        _stateMachine.Enter<BattleState>();
    }
    
    private void OnButtonShopPressed()
    {
        _stateMachine.Enter<ShoppingState>();
    }
    
    public void OnExit()
    {
        MetaGameController.ButtonShopPressed -= OnButtonShopPressed;
    }
    
    public void Dispose()
    {
    }
}