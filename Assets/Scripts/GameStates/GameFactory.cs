using BattleCharacteristics;
using ConfigurationProviders;
using UnityEngine;

public class GameFactory : IGameFactory
{
    private readonly IAssetProvider _assetProvider;

    public GameFactory(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }
    
    public ICoroutineService CreateCoroutineService()
    {
        return new GameObject().AddComponent<CoroutineService>();
    }

    public Player.Player CreatePlayer(Vector3 position, BattleCharacteristicsManager battleCharacteristicsManager)
    {
        var inputService = ServiceLocator.Instance.GetSingle<IInputService>();
        var playerPrefab = _assetProvider.Load<Player.Player>(ResourcesPaths.PLAYER);
        
        var player = Object.Instantiate(playerPrefab, position, Quaternion.identity);
        player.Initialize(inputService, battleCharacteristicsManager);
        
        return player;
    }
    
    public IConfigurationProvider GetConfigurationProvider()
    {
        var configurationProvider = Resources.Load<ConfigurationProvider>(ResourcesPaths.CONFIGURATION_PROVIDER);
        configurationProvider.Initialize();
        return configurationProvider;
    }
}