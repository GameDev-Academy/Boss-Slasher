using System;
using SimpleEventBus.Events;

namespace SimpleEventBus
{
    public class SubscriptionHolder<TEventBase> : ISubscriptionHolder, IDisposable
        where TEventBase : EventBase
    {
        private readonly Action<TEventBase> _action;
        private readonly EventBus _eventBus;
        private readonly Type _type;
        private bool _isDisposed;

        public SubscriptionHolder(EventBus eventBus, Type type, Action<TEventBase> action)
        {
            _type = type;
            _eventBus = eventBus;
            _action = action;
        }

        public void Invoke(EventBase eventBase)
        {
            _action.Invoke(eventBase as TEventBase);
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            
            _eventBus.Unsubscribe(_type, this);
            _isDisposed = true;
        }
    }
}