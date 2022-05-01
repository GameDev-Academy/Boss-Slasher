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
    private int _hp = 3;

    private void Awake()
    {
        EventStreams.Game.Publish(new UnitSpawnedEvent());
    }

    private void Start()
    {
        _isDead = new BoolReactiveProperty(false);
        _health = new IntReactiveProperty(_hp);
    }

    public void TakeDamage(int damageValue)
    {
        _health.Value -= damageValue;
        Debug.Log("HP: " + _health.Value);
        if (_health.Value <= 0)
        {
            Destroy(gameObject);
            //var animatorController = (IAnimatorController)GetComponent(typeof(IAnimatorController));
            //animatorController.Die();
            //_isDead.Value = true;
        }
    }

    public void Initialize(int value)
    {
        //_hp = value;
        Debug.Log("HP: " + value);
    }
}