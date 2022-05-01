using UnityEngine;

public class CoroutineService : MonoBehaviour, ICoroutineService
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}