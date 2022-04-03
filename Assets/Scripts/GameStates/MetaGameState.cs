using ConfigurationProviders;
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
        
        //TODO: Show loading screen
        //TODO: При нажатии кнопки Магазина оружия сюда прилетает ивент или сделать колбек  на метод OnWeaponShopButtonPressed
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
    
    private void OnWeaponShopButtonPressed()
    {
        _stateMachine.Enter<ShoppingState>();
    }
    
    public void OnExit()
    {
    }
    
    public void Dispose()
    {
    }
}