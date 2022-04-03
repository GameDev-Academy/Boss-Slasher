using UniRx;
using UnityEngine;

public interface ISceneLoadingService
{
    AsyncSubject<T> LoadSceneAndFind<T>(string sceneName) 
        where T : MonoBehaviour;
}