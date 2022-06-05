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
        private float _speedRotation = 2f;

        private float _time;

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
            
            var nearestTarget = _targetProvider.GetNearestTarget();
            var targetPosition = nearestTarget.transform.position;
            var position = transform.position;
            var relativePosition = new Vector3(targetPosition.x - position.x, 0f, targetPosition.z - position.z);
            var targetRotation = Quaternion.LookRotation(relativePosition);
            _time += Time.deltaTime * _speedRotation;

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _time);

            return _time > 1 ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnEnd()
        {
            base.OnEnd();
            _time = 0f;
        }
    }
}