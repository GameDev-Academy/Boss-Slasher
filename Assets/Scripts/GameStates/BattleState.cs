using ConfigurationProviders;
using Events;
using IngameStateMachine;
using UniRx;
using User;

public class BattleState : IState
{
    private StateMachine _stateMachine;
    private readonly ISceneLoadingService _sceneLoader;
    
    public BattleState(ISceneLoadingService sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _sceneLoader.LoadScene(SceneNames.BATTLE_SCENE);
    }

    public void OnExit()
    {
    }
    
    public void Dispose()
    {
    }
}