using System;
using BehaviorDesigner.Runtime.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Enemy.AI
{
    /// <summary>
    /// Разворачивает объект к цели
    /// </summary>
    [UsedImplicitly]
    [Serializable]
    public sealed class LookAtTarget : Action
    {
        [SerializeField]
        private GameObject _forward;
        
        [SerializeField]
        private float _rotationDuration = 0.25f;

        private float _currentRotationTime;

        private NavMeshAgent _navMesh;
        private ITargetProvider _targetProvider;

        public override void OnStart()
        {
            base.OnStart();
            
            _navMesh = GetComponent<NavMeshAgent>();
            _targetProvider = gameObject.GetComponent<ITargetProvider>();
        }

        public override TaskStatus OnUpdate()
        {
            base.OnUpdate();

            if (!_targetProvider.HasAnyTarget())
            {
                return TaskStatus.Failure;
            }
            
            var lookRotation = GetLookRotation();
            UpdateRotation(lookRotation);

            return _currentRotationTime > _rotationDuration ? TaskStatus.Success : TaskStatus.Running;
        }

        private void UpdateRotation(Quaternion targetRotation)
        {
            _currentRotationTime += Time.deltaTime;
            
            var startRotation = transform.rotation;
            var rotationProgress = _currentRotationTime / _rotationDuration;

            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, rotationProgress);
        }

        private Quaternion GetLookRotation()
        {
            var nearestTarget = _targetProvider.GetNearestTarget();
            var targetPosition = nearestTarget.transform.position;
            var position = transform.position;
            var relativePosition = new Vector3(targetPosition.x - position.x, 0f, targetPosition.z - position.z);
            var targetRotation = Quaternion.LookRotation(relativePosition);
            
            return targetRotation;
        }

        public override void OnEnd()
        {
            base.OnEnd();
            _currentRotationTime = 0f;
        }
    }
}