using BehaviorDesigner.Runtime;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [FormerlySerializedAs("_healthBehaviour")]
        [SerializeField]
        private HealthHandler _healthHandler;
        
        [SerializeField]
        private BehaviorTree _defenceAreaBehaviour;

        [SerializeField]
        private BehaviorTree _deathBehaviour;

        private void Awake()
        {
            _defenceAreaBehaviour.EnableBehavior();
            
            _healthHandler.IsDead
                .Where(isDead => isDead)
                .Subscribe(_ => OnEnemyDied());
        }

        private void OnEnemyDied()
        {
            _defenceAreaBehaviour.DisableBehavior();
            _deathBehaviour.EnableBehavior();
        }
    }
}