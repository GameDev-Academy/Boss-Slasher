using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class TriggerTargetProvider : MonoBehaviour, ITargetProvider
    {
        [SerializeField]
        private TriggerObserver _triggerObserver;

        private readonly List<GameObject> _targets = new();

        private void Awake()
        {
            _triggerObserver.TriggerEnter += OnEnemyEnterTrigger;
            _triggerObserver.TriggerExit += OnEnemyExitTrigger;
        }

        private void OnEnemyExitTrigger(Collider enemyCollider)
        {
            _targets.Remove(enemyCollider.gameObject);
        }

        private void OnEnemyEnterTrigger(Collider enemyCollider)
        {
            _targets.Add(enemyCollider.gameObject);
        }

        public GameObject GetNearestTarget()
        {
            GameObject nearestTarget = null;
            float nearestTargetDistance = float.MaxValue;
            foreach (var currentTarget in _targets)
            {
                var currentTargetPosition = currentTarget.transform.position;
                var currentDistance = Vector3.Distance(currentTargetPosition, transform.position);

                if (currentDistance < nearestTargetDistance)
                {
                    nearestTargetDistance = currentDistance;
                    nearestTarget = currentTarget;
                }
            }

            return nearestTarget;
        }

        public bool HasAnyTarget()
        {
            return _targets.Count > 0;
        }
    }
}