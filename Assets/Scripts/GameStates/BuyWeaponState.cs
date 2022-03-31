using ConfigurationProviders;
using IngameStateMachine;

public class BuyWeaponState : IState
{
    private StateMachine _stateMachine;
    private IConfigurationProvider _configurationProvider;
    private LoadingScene _loadingScene;
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
        _loadingScene.Initialize(GlobalConstants.WEAPON_MENU_SCENE);
        //При нажатии кнопки выхода из Магазина оружия сюда прилетает ивент
        //ButtonController.WeaponShopExitButtonPressed += OnWeaponShopExitButtonPressed;
    }

    public void OnExit()
    {
    }
}