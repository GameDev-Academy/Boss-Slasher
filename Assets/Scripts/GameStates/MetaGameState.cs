using ConfigurationProviders;
using IngameStateMachine;
using UnityEngine.SceneManagement;

public class MetaGameState : IState
{
    private StateMachine _stateMachine;
    private IConfigurationProvider _configurationProvider;

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
        SceneManager.LoadScene("Main_menu");
        
        // 2. Вызываем у MetaGameManager.Ininitialize и передаем туда userProfile

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