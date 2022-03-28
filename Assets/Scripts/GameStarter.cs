using UnityEngine;
using IngameStateMachine;

public class GameStarter : MonoBehaviour
{
    private StateMachine _stateMachine;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        var userProfile = new UserProfile();
        
        var states = new IState[]
        {
            new BoostrapState(userProfile),
            new MetaGameState(userProfile),
            new BattleState(userProfile)
        };

        _stateMachine = new StateMachine(states);
        _stateMachine.Initialize();
        _stateMachine.Enter<BoostrapState>();
    }
}