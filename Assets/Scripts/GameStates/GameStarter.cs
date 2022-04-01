using ConfigurationProviders;
using UnityEngine;
using IngameStateMachine;
using User;

public class GameStarter : MonoBehaviour
{
    [SerializeField] 
    private ConfigurationProvider _configurationProvider;
    
    private StateMachine _stateMachine;
    private SceneLoader _sceneLoader;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        var userProfileService = new UserProfileService();
        
        _sceneLoader = new SceneLoader();
        _sceneLoader.Initialize(this);
        
        var states = new IState[]
        {
            new BoostrapState(userProfileService, _configurationProvider),
            new MetaGameState(_configurationProvider, _sceneLoader),
            new ShoppingState(_configurationProvider, _sceneLoader),
            new BattleState(_configurationProvider, _sceneLoader)
        };

        _stateMachine = new StateMachine(states);
        _stateMachine.Initialize();
        
        _stateMachine.Enter<BoostrapState>();
    }
}