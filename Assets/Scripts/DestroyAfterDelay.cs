using ScreenManager;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [SerializeField] private float _timeDelay = 1f;

    private void Awake()
    {
        this.DoAfterDelay(() => Destroy(gameObject), _timeDelay);
    }
}