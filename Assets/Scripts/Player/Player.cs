using BattleCharacteristics;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Player - класс медиатор, хранит ссылки на все сервисы, системы и контроллеры, завязанные на игроке,
/// и решает зависимости между ними
/// </summary>
public class Player : MonoBehaviour
{
    [FormerlySerializedAs("_movementSystem")]
    [SerializeField] private MovementHandler _movementHandler;
    [SerializeField] private PlayerAnimationController _animationController;


    public void Initialize(IInputService inputService, BattleCharacteristicsManager battleCharacteristicsManager)
    {
        var speedAsReactiveProperty = battleCharacteristicsManager.GetChatacteristic(CharacteristicType.Speed).Value;
        _movementHandler.Initialize(inputService, speedAsReactiveProperty);
        _animationController.Initialize(inputService);
    }
}