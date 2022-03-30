using ConfigurationsProviders;
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

        var userProfile = new UserProfile(_configurationProvider.CharacteristicsSettingsProvider);
        
        var states = new IState[]
        {
            new BoostrapState(userProfile, _configurationProvider),
            new MetaGameState(userProfile, _configurationProvider),
            new BattleState(userProfile, _configurationProvider)
        };

        _stateMachine = new StateMachine(states);
        _stateMachine.Initialize();
        _stateMachine.Enter<BoostrapState>();
    }
}