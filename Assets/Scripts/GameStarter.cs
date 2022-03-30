using ConfigurationsProviders;
using UnityEngine;
using IngameStateMachine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] 
    private ConfigurationsProvider _configurationsProvider;
    
    private StateMachine _stateMachine;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        var userProfile = new UserProfile(_configurationsProvider.CharacteristicsSettingsProvider);
        
        var states = new IState[]
        {
            new BoostrapState(userProfile, _configurationsProvider),
            new MetaGameState(userProfile, _configurationsProvider),
            new BattleState(userProfile, _configurationsProvider)
        };

        _stateMachine = new StateMachine(states);
        _stateMachine.Initialize();
        _stateMachine.Enter<BoostrapState>();
    }
}