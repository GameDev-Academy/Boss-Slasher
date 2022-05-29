using UniRx;
using UnityEngine;

public interface ISceneLoadingService : IService
{
    AsyncSubject<Unit> LoadScene(string sceneName);
}