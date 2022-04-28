using UnityEngine;

/// <summary>
/// Компонент объекта камеры для следования за игроком
/// </summary>
public class TargetFollowingCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = .5f;
    
    private Transform target;

    
    private void LateUpdate()
    {
        if (target == null) return;

        var desiredposition = target.position + offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredposition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void SetTarget(Transform targetToFollow)
    {
        target = targetToFollow;
    }
}
