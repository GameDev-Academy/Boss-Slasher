using System;
using System.Collections.Generic;
using ScreenManager;
using SimpleEventBus.Events;
using SimpleEventBus.Interfaces;
using UnityEngine;

namespace SimpleEventBus
{
    public class EventBus : IDebuggableEventBus
    {
        public Dictionary<Type, List<ISubscriptionHolder>> Subscriptions => _subscriptions;

        private readonly Dictionary<Type, List<ISubscriptionHolder>> _subscriptions;
        private readonly ObjectPool<List<ISubscriptionHolder>> _listPool = new ObjectPool<List<ISubscriptionHolder>>(list => list.Clear());
        
        public EventBus()
        {
            _subscriptions = new Dictionary<Type, List<ISubscriptionHolder>>();
        }

        public IDisposable Subscribe<TEvent>(Action<TEvent> action) where TEvent : EventBase
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var eventType = typeof(TEvent);
            if (!_subscriptions.ContainsKey(eventType))
            {
                _subscriptions.Add(eventType, new List<ISubscriptionHolder>());
            }

            var subscriptionHolder = new SubscriptionHolder<TEvent>(this, eventType, action);
            _subscriptions[eventType].Add(subscriptionHolder);

            return subscriptionHolder;
        }

        public void Unsubscribe(Type eventType, ISubscriptionHolder subscription)
        {
            if (subscription == null)
            {
                throw new ArgumentNullException(nameof(subscription));
            }

            if (eventType == null)
            {
                throw new ArgumentNullException(nameof(eventType));
            }

            if (_subscriptions.ContainsKey(eventType))
            {
                _subscriptions[eventType].Remove(subscription);
            }
        }

        public void Publish<TEvent>(TEvent eventItem) where TEvent : EventBase
        {
            if (eventItem == null)
            {
                throw new ArgumentNullException(nameof(eventItem));
            }

            if (!_subscriptions.ContainsKey(eventItem.GetType()))
            {
                return;
            }

            NotifyAllSubscribers(eventItem);

            EventFactory.Release(eventItem);
        }

        private void NotifyAllSubscribers<TEventBase>(TEventBase eventItem) where TEventBase : EventBase
        {
            var copyOfSubscriptions = _listPool.Get();
            copyOfSubscriptions.AddRange(_subscriptions[eventItem.GetType()]);
            
            foreach (var subscription in copyOfSubscriptions)
            {
                try
                {
                    subscription.Invoke(eventItem);
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception.ToString());
                }
            }
            
            _listPool.Release(copyOfSubscriptions);
        }
    }
}
