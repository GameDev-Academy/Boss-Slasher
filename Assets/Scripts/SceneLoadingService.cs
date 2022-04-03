using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingService : ISceneLoadingService
{
    private readonly MonoBehaviour _coroutinesHolder;

    public SceneLoadingService(MonoBehaviour coroutinesHolder)
    {
        _coroutinesHolder = coroutinesHolder;
    }

    public AsyncSubject<T> LoadSceneAndFind<T>(string sceneName) where T : MonoBehaviour
    {
        var result = new AsyncSubject<T>();
        _coroutinesHolder.StartCoroutine(LoadSceneAndFind(sceneName, result));
        return result;
    }

    private IEnumerator LoadSceneAndFind<T>(string sceneName, AsyncSubject<T> result) 
        where T : MonoBehaviour
    {
        yield return SceneManager.LoadSceneAsync(sceneName);

        var script = FindRootWith<T>(sceneName);
        if (script == null)
        {
            result.OnError(new Exception($"Script '{typeof(T).FullName}' wasn't found on the scene '{sceneName}'"));
        }
        else
        {
            result.OnNext(script);
            result.OnCompleted();
        }
    }

    private T FindRootWith<T>(string sceneName)
        where T : MonoBehaviour
    {
        var scene = SceneManager.GetSceneByName(sceneName);
        foreach (var rootGameObject in scene.GetRootGameObjects())
        {
            var seekingObject = rootGameObject.GetComponent<T>();
            if (seekingObject != null)
            {
                return seekingObject;
            }
        }

        return null;
    }

}