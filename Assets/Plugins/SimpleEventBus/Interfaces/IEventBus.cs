using System;
using System.Collections.Generic;
using SimpleEventBus.Events;

namespace SimpleEventBus.Interfaces
{
    public interface IDebuggableEventBus : IEventBus
    {
        Dictionary<Type, List<ISubscriptionHolder>> Subscriptions { get; }
    }
    
    public interface IEventBus
    {
        IDisposable Subscribe<TEventBase>(Action<TEventBase> action) where TEventBase : EventBase;
        void Publish<TEventBase>(TEventBase eventItem) where TEventBase : EventBase;
    }
}
