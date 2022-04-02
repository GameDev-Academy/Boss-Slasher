using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Enemy.CustomBehavior
{
    public class FollowToTarget : Action
    {
        public SharedGameObject Target; //todo can change to class Player

        private NavMeshAgent _navMesh;
        
        
        public override void OnAwake()
        {
            _navMesh = GetComponent<NavMeshAgent>();
        }
        
        public override TaskStatus OnUpdate()
        {
            if (HasArrived())
            {
                return TaskStatus.Success;
            }

            _navMesh.SetDestination(TargetAccept());
            return TaskStatus.Running;
        }

        private Vector3 TargetAccept()
        {
            return Target != null ? Target.Value.transform.position : Vector3.zero;
        }
        
        private bool HasArrived()
        {
            var remainingDistance = Vector3.Distance(transform.position, Target.Value.transform.position);
            return remainingDistance <= _navMesh.stoppingDistance; //todo _navMesh.remainingDistance always get 0
        }
    }
}