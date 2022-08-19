using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Компонент объекта камеры для следования за игроком
/// </summary>
public class TargetFollowingCamera : MonoBehaviour
{
    [FormerlySerializedAs("offset")]
    [SerializeField] private Vector3 _offset;
    [FormerlySerializedAs("smoothSpeed")]
    [SerializeField] private float _smoothSpeed = .5f;
    
    private Transform _target;

    
    public void SetTarget(Transform targetToFollow)
    {
        _target = targetToFollow;
    }

    private void LateUpdate()
    {
        if (_target == null) return;

        var desiredPosition = _target.position + _offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothedPosition;
    }
}
