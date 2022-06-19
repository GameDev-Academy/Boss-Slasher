using UniRx;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public IReadOnlyReactiveProperty<bool> IsDead => _isDead;

    [SerializeField] // todo: temp
    private BoolReactiveProperty _isDead;
}