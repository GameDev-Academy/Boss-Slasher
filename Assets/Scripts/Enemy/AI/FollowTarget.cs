using System;
using BehaviorDesigner.Runtime.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Enemy.AI
{
    /// <summary>
    /// Класс отвечающий за логику следования к цели
    /// </summary>
    [UsedImplicitly]
    [Serializable]
    public sealed class FollowTarget : EnemyAction
    {
        [SerializeField]
        private SphereCollider _aggroCollider;

        public override void OnStart()
        {
            _navMesh.isStopped = false;
        }

        public override TaskStatus OnUpdate()
        {
            _navMesh.SetDestination(Target.transform.position);

            if (HasArrived())
            {
                return TaskStatus.Success;
            }

            if (IsPlayerFarAway())
            {
                return TaskStatus.Failure;
            }

            return TaskStatus.Running;
        }

        private bool IsPlayerFarAway()
        {
            return _navMesh.remainingDistance > _aggroCollider.radius;
        }

        private bool HasArrived()
        {
            return _navMesh.remainingDistance <= _navMesh.stoppingDistance;
        }
    }
}