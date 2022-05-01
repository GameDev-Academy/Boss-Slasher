using BattleCharacteristics;
using DealAndTakeDamage;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Слушает ивенты и прокидывает характеристики
    /// </summary>
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private AggroRangeObserver _aggroRangeObserver;
        [SerializeField] 
        private AttackBehaviour _attackBehaviour;
        [SerializeField]
        private HealthBehaviour _healthBehaviour;

        private BattleCharacteristicsManager _battleCharacteristicsManager;

        private void Awake()
        {
            _aggroRangeObserver.TriggerEnter += _attackBehaviour.StartAttack;
            _aggroRangeObserver.TriggerExit += _attackBehaviour.StopAttack;
            _attackBehaviour.Initialize(_battleCharacteristicsManager.GetChatacteristic(CharacteristicType.Damage).Value.Value);
            _healthBehaviour.Initialize(_battleCharacteristicsManager.GetChatacteristic(CharacteristicType.Health).Value.Value);
        }
        
        public void Initialize(BattleCharacteristicsManager battleCharacteristicsManager)
        {
            _battleCharacteristicsManager = battleCharacteristicsManager;
        }
    }
}