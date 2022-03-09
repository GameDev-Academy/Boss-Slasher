using System;
using System.Collections.Generic;

namespace SimpleEventBus.Events
{
    public static class EventFactory
    {
        private static readonly Dictionary<Type, Stack<EventBase>> _eventCache = new Dictionary<Type, Stack<EventBase>>();
        private static readonly HashSet<EventBase> _usedEvents = new HashSet<EventBase>();
    
        public static T Create<T>() where T : EventBase, new()
        {
            var eventCache = GetEventCache(typeof(T));
            var eventData = eventCache.Count == 0 ? new T() : (T)eventCache.Pop();

            _usedEvents.Add(eventData);
        
            return eventData;
        }

        public static void Release<T>(T eventData) where T : EventBase
        {
            if (!_usedEvents.Contains(eventData))
            {
                return;
            }
        
            GetEventCache(eventData.GetType()).Push(eventData);
            _usedEvents.Remove(eventData);
        }

        private static Stack<EventBase> GetEventCache(Type type) 
        {
            if (!_eventCache.TryGetValue(type, out var cache))
            {
                _eventCache.Add(type, cache = new Stack<EventBase>());
            }

            return cache;
        }
    }
}