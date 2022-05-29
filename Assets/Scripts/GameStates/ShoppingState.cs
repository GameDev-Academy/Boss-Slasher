using Events;
using IngameStateMachine;
using UniRx;

public class ShoppingState : IState
{
    private readonly ISceneLoadingService _sceneLoader;
    private StateMachine _stateMachine;
    private CompositeDisposable _subscription;
    
    public ShoppingState(ISceneLoadingService sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
   
    public void OnEnter()
    {
        _sceneLoader.LoadScene(SceneNames.WEAPON_MENU_SCENE).Subscribe(_ => {});
        
        _subscription = new CompositeDisposable
        {
            EventStreams.UserInterface.Subscribe<CloseShopEvent>(CloseShopHandler)
        };
    }
   
    private void CloseShopHandler(CloseShopEvent eventData)
    {
        _stateMachine.Enter<MetaGameState>();
    }
    
    public void OnExit()
    {
    }
    
    public void Dispose()
    {
        _subscription?.Dispose();
    }
}