using BehaviorDesigner.Runtime.Tasks;
using Events;
using UniRx;
using UnityEngine.AI;

namespace Enemy.AI
{
    [TaskCategory("EnemyAction")]
    public class EnemyAction : Action
    {
        protected Player Target { get; private set; }
        protected NavMeshAgent _navMesh;

        private CompositeDisposable _subscriptions;

        public override void OnAwake()
        {
            base.OnAwake();
            _navMesh = GetComponent<NavMeshAgent>();
            SubscribeEvents();
        }

        public override void OnBehaviorRestart()
        {
            base.OnBehaviorRestart();
            SubscribeEvents();
        }

        public override void OnBehaviorComplete()
        {
            base.OnBehaviorComplete();
            _subscriptions?.Dispose();
        }

        private void SetTarget(PlayerInstantiatedEvent eventData)
        {
            Target = eventData.Player;
        }

        private void SubscribeEvents()
        {
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<PlayerInstantiatedEvent>(SetTarget)
            };
        }
    }
}