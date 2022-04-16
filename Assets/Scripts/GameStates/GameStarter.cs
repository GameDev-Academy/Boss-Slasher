using ConfigurationProviders;
using IngameStateMachine;
using UnityEngine;
using User;

namespace GameStates
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] 
        private ConfigurationProvider _configurationProvider;

        [SerializeField] private Player.Player _playerPrefab;

        private StateMachine _stateMachine;
        private SceneLoadingService _sceneLoader;
        private UserProfileService _userProfileService;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _sceneLoader = new SceneLoadingService(this);
            _userProfileService = new UserProfileService(_configurationProvider);
        
            var userProfile = _userProfileService.GetProfile();
            var characteristicsSettingsProvider = _configurationProvider.CharacteristicsSettings;
        
            var moneyService = new MoneyService(userProfile);
            var characteristicService = new CharacteristicsService(userProfile, 
                characteristicsSettingsProvider,
                moneyService);


            var states = new IState[]
            {
                new BoostrapState(_configurationProvider),
                new MetaGameState(_configurationProvider, _sceneLoader, characteristicService, moneyService),
                new ShoppingState(_configurationProvider, _sceneLoader, moneyService),
                new BattleState(_configurationProvider, characteristicService, _sceneLoader, _playerPrefab)
            };

            _stateMachine = new StateMachine(states);
            _stateMachine.Initialize();
            _stateMachine.Enter<BoostrapState>();
        }

        public void OnDestroy()
        {
            _userProfileService.Dispose();
        }
    }
}