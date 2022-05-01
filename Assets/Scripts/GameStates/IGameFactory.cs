using BattleCharacteristics;
using ConfigurationProviders;
using UnityEngine;

public interface IGameFactory : IService
{
    ICoroutineService CreateCoroutineService();
    IConfigurationProvider GetConfigurationProvider();
    Player CreatePlayer(Vector3 position, BattleCharacteristicsManager battleCharacteristicsManager);
}