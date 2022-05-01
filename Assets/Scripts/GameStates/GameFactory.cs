using UnityEngine;

public class GameFactory : IGameFactory
{
    public ICoroutineService CreateCoroutineService()
    {
        return new GameObject().AddComponent<CoroutineService>();
    }

    public GameObject CreatePlayer()
    {
        throw new System.NotImplementedException();
    }

    public GameObject CreateEnemy()
    {
        throw new System.NotImplementedException();
    }
}