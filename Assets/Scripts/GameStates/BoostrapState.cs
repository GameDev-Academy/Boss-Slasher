using CharacteristicsSettings;
using ConfigurationProviders;
using IngameStateMachine;
using User;
using WeaponsSettings;

public class BoostrapState : IState
{
    private StateMachine _stateMachine;
    
    private readonly IConfigurationProvider _configurationProvider;

    public BoostrapState(IConfigurationProvider configurationProvider)
    {
        _configurationProvider = configurationProvider;
    }
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public ServiceLocator RegisterServices()
    {
        var serviceLocator = new ServiceLocator();

        // IAssetProvider

        // Configuration Providers:
        // TODO : Load via AssetProvider
        serviceLocator.RegisterSingle(_configurationProvider);
        _configurationProvider.Initialize();

        serviceLocator.RegisterSingle(_configurationProvider.CharacteristicsSettings);
        serviceLocator.RegisterSingle(_configurationProvider.WeaponsSettingsProvider);

        // Services:
        serviceLocator.RegisterSingle<IGameFactory>(new GameFactory());
        
        var gameFactory = serviceLocator.GetSingle<IGameFactory>();
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