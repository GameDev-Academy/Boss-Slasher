using System;
using ScreenManager.Enums;
using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class CloseScreenByGuidEvent : EventBase
    {
        public Guid Guid;
        public bool RemoveFromQueue;
        
        public static CloseScreenByGuidEvent Create(Guid guid, bool removeFromQueue = false)
        {
            var eventData = EventFactory.Create<CloseScreenByGuidEvent>();
            eventData.Guid = guid;
            eventData.RemoveFromQueue = removeFromQueue;
            return eventData;
        }
    }
    
    public class CloseScreensByTypeEvent : EventBase
    {
        public ScreenId ScreenId;
        public bool RemoveFromQueue;

        public static CloseScreensByTypeEvent Create(ScreenId screenId, bool removeFromQueue = false)
        {
            var eventData = EventFactory.Create<CloseScreensByTypeEvent>();
            eventData.ScreenId = screenId;
            eventData.RemoveFromQueue = removeFromQueue;
            return eventData;
        }
    }
}
