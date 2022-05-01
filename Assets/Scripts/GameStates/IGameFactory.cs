using UnityEngine;

public interface IGameFactory : IService
{
    ICoroutineService CreateCoroutineService();
    GameObject CreatePlayer();
    GameObject CreateEnemy();
}