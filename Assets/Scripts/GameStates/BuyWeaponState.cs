using ConfigurationProviders;
using IngameStateMachine;

public class BuyWeaponState : IState
{
    private StateMachine _stateMachine;
    private IConfigurationProvider _configurationProvider;
    private SceneLoader _sceneLoader;
    public BuyWeaponState(IConfigurationProvider configurationProvider)
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

    private void OnWeaponShopExitButtonPressed()
    {
        _stateMachine.Enter<MetaGameState>();
    }
    public void OnEnter()
    {
        _sceneLoader.Load(NameScenesConstants.WEAPON_MENU_SCENE); 
    //TODO: При нажатии кнопки выхода из Магазина сюда прилетает ивент или сделать колбек на метод OnWeaponShopExitButtonPressed
    //ButtonController.WeaponShopExitButtonPressed += OnWeaponShopExitButtonPressed;
    }

    public void OnExit()
    {
    }
}