using CharacteristicsSettings;
using ConfigurationProviders;
using IngameStateMachine;
using User;
using WeaponsSettings;

namespace GameStates
{
    public class BoostrapState : IState
    {
        private StateMachine _stateMachine;

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public ServiceLocator RegisterServices()
        {
            var serviceLocator = new ServiceLocator();

            // Configuration Providers:
            serviceLocator.RegisterSingle<IAssetProvider>(new AssetProvider());

            var assetProvider = serviceLocator.GetSingle<IAssetProvider>();
            serviceLocator.RegisterSingle<IGameFactory>(new GameFactory(assetProvider));
           
            var gameFactory = serviceLocator.GetSingle<IGameFactory>();
            serviceLocator.RegisterSingle(gameFactory.GetConfigurationProvider());

            var configurationProvider = serviceLocator.GetSingle<IConfigurationProvider>();
            serviceLocator.RegisterSingle(configurationProvider.CharacteristicsSettings);
            serviceLocator.RegisterSingle(configurationProvider.WeaponsSettingsProvider);

            // Services:
            serviceLocator.RegisterSingle(gameFactory.CreateCoroutineService());

            var coroutineService = serviceLocator.GetSingle<ICoroutineService>();
            serviceLocator.RegisterSingle<ISceneLoadingService>(new SceneLoadingService(coroutineService));

            var weaponsSettingsProvider = serviceLocator.GetSingle<IWeaponsSettingsProvider>();
            serviceLocator.RegisterSingle<IUserProfileService>(new UserProfileService(weaponsSettingsProvider));
        
            var userProfile = serviceLocator.GetSingle<IUserProfileService>().GetProfile();
            serviceLocator.RegisterSingle<IMoneyService>(new MoneyService(userProfile));

            serviceLocator.RegisterSingle<ICharacteristicsService>(new CharacteristicsService(userProfile,
                serviceLocator.GetSingle<ICharacteristicsSettingsProvider>(),
                serviceLocator.GetSingle<IMoneyService>()));
        
            serviceLocator.RegisterSingle<IWeaponsService>(new WeaponsService(weaponsSettingsProvider, 
                userProfile, serviceLocator.GetSingle<IMoneyService>()));

            serviceLocator.RegisterSingle<IInputService>(new InputService());
            

            return serviceLocator;
        }
    
        public void OnEnter()
        {
            _stateMachine.Enter<MetaGameState>();
        }
    
        public void OnExit()
        {
        }
    
        public void Dispose()
        {
        }
    }
}