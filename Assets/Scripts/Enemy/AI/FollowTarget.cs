using System;
using BehaviorDesigner.Runtime.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Enemy.AI
{
    /// <summary>
    /// Класс отвечающий за логику следования к цели
    /// </summary>
    [UsedImplicitly]
    [Serializable]
    public sealed class FollowTarget : Action
    {
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
            
            var target = _targetProvider.GetNearestTarget();
            _navMesh.SetDestination(target.transform.position);

            if (HasReachedTarget())
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }

        private bool HasReachedTarget()
        {
            return !_navMesh.pathPending &&
                   _navMesh.remainingDistance <= _navMesh.stoppingDistance;
        }
    }
}