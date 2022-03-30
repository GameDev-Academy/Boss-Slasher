using ConfigurationsProviders;
using UnityEngine;
using IngameStateMachine;

public class GameStarter : MonoBehaviour
{
    public IConfigurationProvider ConfigurationProvider => configurationProvider;
    
    [SerializeField] 
    private ConfigurationProvider configurationProvider;
    
    private StateMachine _stateMachine;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        var userProfile = new UserProfile(configurationProvider.CharacteristicsSettingsProvider);
        
        var states = new IState[]
        {
            new BoostrapState(userProfile, configurationProvider),
            new MetaGameState(userProfile, configurationProvider),
            new BattleState(userProfile, configurationProvider)
        };

        _stateMachine = new StateMachine(states);
        _stateMachine.Initialize();
        _stateMachine.Enter<BoostrapState>();
    }
}