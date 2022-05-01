using ConfigurationProviders;
using UnityEngine;
using IngameStateMachine;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    private ConfigurationProvider _configurationProvider;

    private StateMachine _stateMachine;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        var boostrapState = new BoostrapState(_configurationProvider);
        var serviceLocator = boostrapState.RegisterServices();
        
        var sceneLoadingService = serviceLocator.GetSingle<ISceneLoadingService>();
        var metaGameState = new MetaGameState(sceneLoadingService);
        var shoppingState = new ShoppingState(sceneLoadingService);
        var battleState = new BattleState(sceneLoadingService);
      
        _stateMachine = new StateMachine(boostrapState, metaGameState, shoppingState, battleState);
        _stateMachine.Initialize();
        _stateMachine.Enter<BoostrapState>();
    }

    public void OnDestroy()
    {
        ServiceLocator.Instance.Dispose();
    }
}