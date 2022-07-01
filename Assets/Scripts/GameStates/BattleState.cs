using BattleLoot;
using ConfigurationProviders;
using Events;
using IngameStateMachine;
using UniRx;
using User;

public class BattleState : IState
{
    private StateMachine _stateMachine;
    private readonly ISceneLoadingService _sceneLoader;
    private CompositeDisposable _subscription;
    

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
        
        _subscription = new CompositeDisposable
        {
            EventStreams.UserInterface.Subscribe<OpenMetaGameEvent>(OpenMetaGameHandler),
        };
    }

    public void OnExit()
    {
        _subscription?.Dispose();
    }

    public void Dispose()
    {
    }

    private void OpenMetaGameHandler(OpenMetaGameEvent obj)
    {
        _stateMachine.Enter<MetaGameState>();
    }
}