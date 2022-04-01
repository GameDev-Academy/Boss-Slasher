using ConfigurationProviders;
using UnityEngine;
using IngameStateMachine;
using User;

public class GameStarter : MonoBehaviour
{
    [SerializeField] 
    private ConfigurationProvider _configurationProvider;
    
    private SceneLoader _sceneLoader;
    private StateMachine _stateMachine;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        var userProfileService = new UserProfileService();

        var states = new IState[]
        {
            new BoostrapState(userProfileService, _configurationProvider),
            new MetaGameState(_configurationProvider, _sceneLoader),
            new BuyWeaponState(_configurationProvider),
            new BattleState(_configurationProvider)
        };

        _stateMachine = new StateMachine(states);
        _stateMachine.Initialize();

        _sceneLoader = new SceneLoader();
        _sceneLoader.Initialize(this);
        
        _stateMachine.Enter<BoostrapState>();
    }
}