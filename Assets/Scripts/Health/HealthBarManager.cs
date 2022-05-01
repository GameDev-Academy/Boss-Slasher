using Events;
using UniRx;

namespace Health
{
    public class HealthBarManager
    {
        private CompositeDisposable _subscriptions;
        
        private void Awake()
        {
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<UnitSpawnedEvent>(AddHealthBar)
            };
        }

        private void AddHealthBar(UnitSpawnedEvent eventData)
        {
            // добавить хелсбар
        }
        
        private void OnDestroy()
        {
            _subscriptions?.Dispose();
        }
    }
}