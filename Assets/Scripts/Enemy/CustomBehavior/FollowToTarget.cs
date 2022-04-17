using BehaviorDesigner.Runtime.Tasks;
using Events;
using JetBrains.Annotations;
using UniRx;
using UnityEngine.AI;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Enemy.CustomBehavior
{
    /// <summary>
    /// Реализует логику следования к цели 
    /// </summary>
    [UsedImplicitly]
    public class FollowToTarget : Action
    {
        private Player.Player _target;
        private NavMeshAgent _navMesh;
        private CompositeDisposable _subscriptions;

        public override void OnAwake()
        {
            base.OnAwake();
            _navMesh = GetComponent<NavMeshAgent>();
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<PlayerInstantiatedEvent>(SetTarget)
            };
        }

        public override void OnBehaviorComplete()
        {
            base.OnBehaviorComplete();
            _subscriptions?.Dispose();
        }

        private void SetTarget(PlayerInstantiatedEvent eventData)
        {
            _target = eventData.Player;
        }

        public override TaskStatus OnUpdate()
        {
            _navMesh.isStopped = false;
            _navMesh.SetDestination(_target.transform.position);
            return TaskStatus.Success;
        }
    }
}