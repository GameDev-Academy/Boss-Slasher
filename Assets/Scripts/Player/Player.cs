using BattleCharacteristics;
using UnityEngine;

/// <summary>
/// Player - класс медиатор, хранит ссылки на все сервисы, системы и контроллеры, завязанные на игроке,
/// и решает зависимости между ними
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] private MovementSystem _movementSystem;
    [SerializeField] private PlayerAnimationController _animationController;
    
    private BattleCharacteristicsManager _battleCharacteristicsManager;

    
    public void Initialize(IInputService inputService, BattleCharacteristicsManager battleCharacteristicsManager)
    {
        _battleCharacteristicsManager = battleCharacteristicsManager;
        var speedAsReactiveProperty = _battleCharacteristicsManager.GetChatacteristic(CharacteristicType.Speed).Value;
        _movementSystem.Initialize(inputService, speedAsReactiveProperty);
        _animationController.Initialize(inputService);
    }
}
