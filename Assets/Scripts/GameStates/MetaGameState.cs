using ConfigurationProviders;
using IngameStateMachine;

public class MetaGameState : IState
{
    private StateMachine _stateMachine;
    private IConfigurationProvider _configurationProvider;
    private MetaGameController _metaGameController;

    public MetaGameState(IConfigurationProvider configurationProvider)
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
        //инициализируем контроллеры параметрами игрока
        _metaGameController.Initialize(_configurationProvider);
        
        /*
         При нажатии кнопки Магазина оружия сюда прилетает ивент
         MainMenuButtonController.WeaponShopButtonPressed += OnWeaponShopButtonPressed;
         -//- c кнопкой начала игры
         MainMenuButtonController.StartBattle += StartBattleHandler;
        */
    }
    public void OnExit()
    {
        //MainMenuButtonController.WeaponShopButtonPressed -= OnWeaponShopButtonPressed;
        //MainMenuButtonController.StartBattle -= StartBattleHandler;
    }
}