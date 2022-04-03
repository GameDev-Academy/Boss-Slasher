using ConfigurationProviders;
using IngameStateMachine;using UnityEngine;

public class BattleState : IState
{
    private StateMachine _stateMachine;
    private BattleController _battleController;
    private IConfigurationProvider _configurationProvider;
    private SceneLoader _sceneLoader;
    
    public BattleState(IConfigurationProvider configurationProvider, SceneLoader sceneLoader)
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
        _sceneLoader.Load(SceneNames.BATTLE_SCENE);
        
        _battleController = Object.FindObjectOfType<BattleController>();
        _battleController.Initialize(_configurationProvider);
    }

    public void OnExit()
    {
    }
    
    public void Dispose()
    {
    }
}