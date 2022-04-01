using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private MonoBehaviour _coroutinesHolder;

    public void Initialize(MonoBehaviour coroutinesHolder)
    {
        _coroutinesHolder = coroutinesHolder;
    }
    
    public void Load(string sceneName)
    {
        _coroutinesHolder.StartCoroutine(LoadingScene(sceneName));
    }
    
    private IEnumerator LoadingScene(string nameScene)
    {
        //TODO: Во время перехода между сценами будет появляться слайдер или темнеть экран, но это еще нужно реализовать!
        var operation = SceneManager.LoadSceneAsync(nameScene);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}