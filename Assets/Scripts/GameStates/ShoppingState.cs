using ConfigurationProviders;
using Events;
using GameControllers;
using IngameStateMachine;
using UniRx;
using User;

public class ShoppingState : IState
{
    private StateMachine _stateMachine;
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IMoneyService _moneyService;
    private IWeaponsService _weaponsService;
    private ISceneLoadingService _sceneLoader;
    
    private CompositeDisposable _subscription;
    
    public ShoppingState(IConfigurationProvider configurationProvider,
        ISceneLoadingService sceneLoader,
        IWeaponsService weaponsService,
        IMoneyService moneyService)
    {
        _configurationProvider = configurationProvider;
        _moneyService = moneyService;
        _weaponsService = weaponsService;
        _sceneLoader = sceneLoader;
    }
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
   
    public void OnEnter()
    {
        _sceneLoader.LoadSceneAndFind<ShoppingScreenController>(SceneNames.WEAPON_MENU_SCENE)
            .Subscribe(OnSceneLoaded);
        
        _subscription = new CompositeDisposable
        {
            EventStreams.UserInterface.Subscribe<CloseShopEvent>(CloseShopHandler)
        };
    }
    
    private void OnSceneLoaded(ShoppingScreenController shoppingScreenController)
    {
        shoppingScreenController.Initialize(_configurationProvider.WeaponsSettingsProvider, _weaponsService, _moneyService);
    }
   
    private void CloseShopHandler(CloseShopEvent eventData)
    {
        _stateMachine.Enter<MetaGameState>();
    }
    
    public void OnExit()
    {
    }
    
    public void Dispose()
    {
    }
}