using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class ScreenOpeningEvent : EventBase
    {
        public ScreenData ScreenData { get; private set; }

        public static ScreenOpeningEvent Create(ScreenData screenData)
        {
            var eventData = EventFactory.Create<ScreenOpeningEvent>();
            eventData.ScreenData = screenData;
            return eventData;
        }
    }
}
