using ConfigurationProviders;
using Events;
using IngameStateMachine;
using User;
using UniRx;

public class MetaGameState : IState
{
    private readonly ISceneLoadingService _sceneLoader;

    private StateMachine _stateMachine;
    private CompositeDisposable _subscription;

    public MetaGameState(ISceneLoadingService sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    public void OnEnter()
    {
        _sceneLoader.LoadScene(SceneNames.METAGAME_SCENE).Subscribe(_ => {});
        
        _subscription = new CompositeDisposable
        {
            EventStreams.UserInterface.Subscribe<OpenShopEvent>(OpenShopHandler),
            EventStreams.UserInterface.Subscribe<StartBattleEvent>(StartBattleHandler)
        };
        
        //TODO: Show loading screen
    }

    private void StartBattleHandler(StartBattleEvent eventData)
    {
        _stateMachine.Enter<BattleState>();
    }
    
    private void OpenShopHandler(OpenShopEvent eventData)
    {
        _stateMachine.Enter<ShoppingState>();
    }
    
    public void OnExit()
    {
        _subscription.Dispose();
    }
    
    public void Dispose()
    {
    }
}