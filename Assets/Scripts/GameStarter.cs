using UnityEngine;
using IngameStateMachine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] 
    private ConfigurationsProvider configurationsProvider;
    
    private StateMachine _stateMachine;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        var userProfile = ScriptableObject.CreateInstance<UserProfile>();
        
        var states = new IState[]
        {
            new BoostrapState(userProfile, configurationsProvider),
            new MetaGameState(userProfile, configurationsProvider),
            new BattleState(userProfile, configurationsProvider)
        };

        _stateMachine = new StateMachine(states);
        _stateMachine.Initialize();
        _stateMachine.Enter<BoostrapState>();
    }
}