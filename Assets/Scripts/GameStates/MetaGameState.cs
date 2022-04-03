using ConfigurationProviders;
using IngameStateMachine;
using UnityEngine;
using User;

public class MetaGameState : IState
{
    private StateMachine _stateMachine;
    
    private readonly ICharacteristicsService _characteristicsService;
    private readonly IMoneyService _moneyService;
    private readonly IConfigurationProvider _configurationProvider;
    private readonly SceneLoader _sceneLoader;
    
    private MetaGameController _metaGameController;

    public MetaGameState(IConfigurationProvider configurationProvider, SceneLoader sceneLoader, ICharacteristicsService characteristicsService, IMoneyService moneyService)
    {
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
        _sceneLoader.Load(SceneNames.METAGAME_SCENE);
        
        _metaGameController = Object.FindObjectOfType<MetaGameController>();
        _metaGameController.Initialize(_configurationProvider, _characteristicsService, _moneyService);
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