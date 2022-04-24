using BehaviorDesigner.Runtime.Tasks;
using Events;
using JetBrains.Annotations;
using SimpleEventBus.Disposables;

namespace Enemy.AI
{
    /// <summary>
    /// При получении ивента о смерти Enemy, включает анимацию смерти и завершает выполнение BehaviorTree
    /// </summary>
    [UsedImplicitly]
    public class EnemyDied : Conditional
    {
        private CompositeDisposable _subscriptions;
        private bool _isDead;

        public override void OnAwake()
        {
            base.OnAwake();
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<EnemyDiedEvent>(EnemyDieEventHandler)
            };
        }

        public override void OnBehaviorComplete()
        {
            base.OnBehaviorComplete();
            _subscriptions?.Dispose();
        }

        public override TaskStatus OnUpdate()
        {
            if (_isDead)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
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