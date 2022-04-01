using ConfigurationProviders;
using IngameStateMachine;

public class MetaGameState : IState
{
    private StateMachine _stateMachine;
    private IConfigurationProvider _configurationProvider;
    private MetaGameController _metaGameController;
    private SceneLoader _sceneLoader;

    public MetaGameState(IConfigurationProvider configurationProvider, SceneLoader sceneLoader, MetaGameController metaGameController)
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
        _sceneLoader.Load(SceneNames.METAGAME_SCENE);
        _metaGameController.Initialize(_configurationProvider);

        //TODO: При нажатии кнопки Магазина оружия сюда прилетает ивент или сделать колбек  на метод OnWeaponShopButtonPressed
        //TODO: При нажатии кнопки Начала игры сюда прилетает ивент или сделать колбек  на метод StartBattleHandler
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