using UniRx;
using UnityEngine;
using Events;

public class HealthBehaviour : MonoBehaviour
{
    /// <summary>
    /// Отвечает за здоровье и смерть
    /// </summary>
    public IReadOnlyReactiveProperty<bool> IsDead => _isDead;
    public IReadOnlyReactiveProperty<int> Health => _health;

    private BoolReactiveProperty _isDead;
    private IntReactiveProperty _health;

    private void Awake()
    {
        EventStreams.Game.Publish(new UnitSpawnedEvent());
    }

    public void TakeDamage(int damageValue)
    {
        _health.Value -= damageValue;
        Debug.Log("HP: " + Health);
        if (_health.Value <= 0)
        {
            Destroy(gameObject);
            _isDead.Value = true;
        }
    }

    public void Initialize(int value)
    {
        _isDead = new BoolReactiveProperty(false);
        _health = new IntReactiveProperty(value);
    }
}