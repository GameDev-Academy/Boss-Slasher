using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace Enemy.CustomBehavior
{
    [TaskCategory("Unity/ResetTarget")]
    public class ResetTarget : Action
    {
        private NavMeshAgent _navMeshAgent;

        public override void OnStart()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            _navMeshAgent.isStopped = true;
            return TaskStatus.Success;
        }
    }
}