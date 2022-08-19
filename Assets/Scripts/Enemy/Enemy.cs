using BehaviorDesigner.Runtime;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private EnemyHealth _enemyHealth;
        
        [SerializeField]
        private BehaviorTree _defenceAreaBehaviour;

        [SerializeField]
        private BehaviorTree _deathBehaviour;

        private void Awake()
        {
            _defenceAreaBehaviour.EnableBehavior();
            
            _enemyHealth.IsDead
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