using ConfigurationProviders;
using GameControllers;
using IngameStateMachine;
using WeaponsSettings;
using UniRx;
using User;

public class ShoppingState : IState
{
    private StateMachine _stateMachine;
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IMoneyService _moneyService;
    private ISceneLoadingService _sceneLoader;
    
    public ShoppingState(
        IConfigurationProvider configurationProvider, 
        ISceneLoadingService sceneLoader,
        IMoneyService moneyService)
    {
        _configurationProvider = configurationProvider;
        _sceneLoader = sceneLoader;
        _moneyService = moneyService;
    }
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
   
    public void OnEnter()
    {
        _sceneLoader.LoadSceneAndFind<ShoppingScreenController>(SceneNames.WEAPON_MENU_SCENE)
            .Subscribe(OnSceneLoaded);
        //TODO: При нажатии кнопки выхода из Магазина сюда прилетает ивент или сделать колбек на метод OnWeaponShopExitButtonPressed
    }
    
    private void OnSceneLoaded(ShoppingScreenController shoppingScreenController)
    {
        shoppingScreenController.Initialize(_moneyService);
    }
   
    private void OnWeaponShopExitButtonPressed()
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