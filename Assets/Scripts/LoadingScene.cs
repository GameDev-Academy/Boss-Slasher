using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public void Initialize(string nameScene)
    {
        StartCoroutine(LoadingGameScene(nameScene));
    }
    
    private IEnumerator LoadingGameScene(string nameScene)
    {
        //мб будет появляться слайдер или темнеть экран..
        var operation = SceneManager.LoadSceneAsync(nameScene);
        while (!operation.isDone)
        {
            //_mainMenuSlider.value = operation.progress;
            yield return null;
        }
    }
}