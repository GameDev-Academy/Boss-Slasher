using ScreenManager.Enums;
using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class BackToPreviousScreenEvent : EventBase
    {
        public ScreenId ScreenId;

        public static BackToPreviousScreenEvent Create(ScreenId screenId)
        {
            var eventData = EventFactory.Create<BackToPreviousScreenEvent>();
            eventData.ScreenId = screenId;
            return eventData;
        }
        
        public static BackToPreviousScreenEvent Create()
        {
            var eventData = EventFactory.Create<BackToPreviousScreenEvent>();
            eventData.ScreenId = ScreenId.None;
            return eventData;
        }
    }
}
