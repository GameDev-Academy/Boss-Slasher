using System.Collections;
using UniRx;
using UnityEngine.SceneManagement;

public class SceneLoadingService : ISceneLoadingService
{
    private readonly ICoroutineService _coroutinesHolder;

    public SceneLoadingService(ICoroutineService coroutinesHolder)
    {
        _coroutinesHolder = coroutinesHolder;
    }

    public AsyncSubject<Unit> LoadScene(string sceneName)
    {
        var result = new AsyncSubject<Unit>();
        _coroutinesHolder.StartCoroutine(LoadScene(sceneName, result));
        return result;
    }

    private IEnumerator LoadScene(string sceneName, AsyncSubject<Unit> result) 
    {
        yield return SceneManager.LoadSceneAsync(sceneName);

        result.OnNext(Unit.Default);
        result.OnCompleted();
    }
}