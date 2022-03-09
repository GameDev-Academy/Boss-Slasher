using ScreenManager.Enums;
using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class TopScreenChangedEvent : EventBase
    {
        public ScreenId Id;

        public static TopScreenChangedEvent Create(ScreenId id)
        {
            var eventData = EventFactory.Create<TopScreenChangedEvent>();
            eventData.Id = id;
            return eventData;
        }
    }
}