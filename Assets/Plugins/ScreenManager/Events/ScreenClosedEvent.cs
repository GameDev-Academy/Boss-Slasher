using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class ScreenClosedEvent : EventBase
    {
        public ScreenData ScreenData { get; private set; }

        public static ScreenClosedEvent Create(ScreenData screen)
        {
            var eventData = EventFactory.Create<ScreenClosedEvent>();
            eventData.ScreenData = screen;
            return eventData;
        }
    }
}