using BehaviorDesigner.Runtime.Tasks;
using JetBrains.Annotations;
using UnityEngine.AI;

namespace Enemy.CustomBehavior
{
    /// <summary>
    /// Проверяет дошел ли Enemy до цели
    /// </summary>
    [UsedImplicitly]
    public class TargetAcquired : Conditional

    {
        private NavMeshAgent _navMesh;

        public override void OnAwake()
        {
            base.OnAwake();
            _navMesh = GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            if (HasArrived())
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }

        private bool HasArrived()
        {
            return _navMesh.remainingDistance <= _navMesh.stoppingDistance;
        }
    }
}