using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class CloseAllScreensEvent : EventBase
    {
        public bool Instantly;
        public bool CleanQueue;

        public static CloseAllScreensEvent Create(bool instantly = true, bool cleanQueue = true)
        {
            var eventData = EventFactory.Create<CloseAllScreensEvent>();
            eventData.Instantly = instantly;
            eventData.CleanQueue = cleanQueue;
            return eventData;
        }
    }
}
