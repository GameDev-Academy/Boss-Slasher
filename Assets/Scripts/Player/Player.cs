using BattleCharacteristics;
using Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    /// <summary>
    /// Player - класс медиатор, хранит ссылки на все сервисы, системы и контроллеры, завязанные на игроке,
    /// и решает зависимости между ними
    /// </summary>
    public class Player : MonoBehaviour
    {
        [FormerlySerializedAs("_movementSystem")] [SerializeField]
        private MovementHandler _movementHandler;
        [SerializeField] private PlayerAnimationController _animationController;
        [FormerlySerializedAs("_heroHealth")] [SerializeField]
        private PlayerHealth playerHealth;
        [SerializeField] private PlayerAttackHandler playerAttackHandler;

        private bool _isDead;


        public void Initialize(IInputService inputService, BattleCharacteristicsManager battleCharacteristicsManager)
        {
            var speedAsReactiveProperty =
                battleCharacteristicsManager.GetChatacteristic(CharacteristicType.Speed).Value;
            _movementHandler.Initialize(inputService, speedAsReactiveProperty);
            _animationController.Initialize(inputService);
            playerAttackHandler.Initialize(_animationController);

            SetMaxHealth(battleCharacteristicsManager);
            playerHealth.IsDead.SubscribeWhenTrue(OnPlayerDied);
        }

        private void SetMaxHealth(BattleCharacteristicsManager battleCharacteristicsManager)
        {
            playerHealth.InitializePlayerHealth(
                battleCharacteristicsManager.GetChatacteristic(CharacteristicType.Health).Value.Value);
        }

        private void OnPlayerDied()
        {
            if (!_isDead)
            {
                _isDead = true;
                _animationController.PlayDeath();
                _movementHandler.enabled = false;
                EventStreams.UserInterface.Publish(new DungeonPassEvent(false));
            }
        }
    }
}