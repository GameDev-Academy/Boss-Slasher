using BehaviorDesigner.Runtime.Tasks;
using JetBrains.Annotations;
using SimpleEventBus.Disposables;

namespace Enemy.CustomBehavior
{
    [UsedImplicitly]
    public class EnemyDeath : Action
    {
        private CompositeDisposable _subscriptions;
        private bool _isDead;

        public override void OnAwake()
        {
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<EnemyDiedEvent>(EnemyDieEventHandler)
            };
        }

        public override TaskStatus OnUpdate()
        {
            if (_isDead == true)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        public override void OnReset()
        {
            _subscriptions.Dispose();
        }

        private void EnemyDieEventHandler(EnemyDiedEvent eventData)
        {
            if (eventData.Enemy.gameObject == gameObject)
            {
                _isDead = true;
            }
        }
    }
}