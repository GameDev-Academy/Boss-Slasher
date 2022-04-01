using ConfigurationProviders;
using IngameStateMachine;

public class MetaGameState : IState
{
    private StateMachine _stateMachine;
    private IConfigurationProvider _configurationProvider;
    private MetaGameController _metaGameController;
    private SceneLoader _sceneLoader;

    public MetaGameState(IConfigurationProvider configurationProvider, SceneLoader sceneLoader)
    {
        _configurationProvider = configurationProvider;
        _sceneLoader = sceneLoader;
    }

    public void Dispose()
    {
    }

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    private void StartBattleHandler()
    {
        _stateMachine.Enter<BattleState>();
    }
    private void OnWeaponShopButtonPressed()
    {
        _stateMachine.Enter<BuyWeaponState>();
    }

    public void OnEnter()
    {
        _sceneLoader.Load(NameScenesConstants.METAGAME_SCENE);
        _metaGameController.Initialize(_configurationProvider);
        
        /*
    TODO: При нажатии кнопки Магазина оружия сюда прилетает ивент или сделать колбек  на метод OnWeaponShopButtonPressed
         MainMenuButtonController.WeaponShopButtonPressed += OnWeaponShopButtonPressed;
    TODO: При нажатии кнопки Начала игры сюда прилетает ивент или сделать колбек  на метод StartBattleHandler
         MainMenuButtonController.StartBattle += StartBattleHandler;
        */
    }
    public void OnExit()
    {
    }
}