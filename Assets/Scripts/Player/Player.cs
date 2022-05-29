using BattleCharacteristics;
using UnityEngine;
using DealAndTakeDamage;

/// <summary>
/// Player - класс медиатор, хранит ссылки на все сервисы, системы и контроллеры, завязанные на игроке,
/// и решает зависимости между ними
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] private MovementSystem _movementSystem;
    [SerializeField] private PlayerAnimationController _animationController;
    [SerializeField] private AggroRangeObserver _aggroRangeObserver;
    [SerializeField] private AttackBehaviour _attackBehaviour;
    [SerializeField] private HealthBehaviour _healthBehaviour;
    
    private BattleCharacteristicsManager _battleCharacteristicsManager;

    private void Awake()
    {
        _aggroRangeObserver.TriggerEnter += _attackBehaviour.StartAttack;
    }
    
    public void Initialize(IInputService inputService, BattleCharacteristicsManager battleCharacteristicsManager)
    {
        _battleCharacteristicsManager = battleCharacteristicsManager;
        var speedAsReactiveProperty = _battleCharacteristicsManager.GetChatacteristic(CharacteristicType.Speed).Value;
        _movementSystem.Initialize(inputService, speedAsReactiveProperty);
        _animationController.Initialize(inputService);
        _attackBehaviour.Initialize(_battleCharacteristicsManager.GetChatacteristic(CharacteristicType.Damage).Value.Value);
        _healthBehaviour.Initialize(_battleCharacteristicsManager.GetChatacteristic(CharacteristicType.Health).Value.Value);
    }
}