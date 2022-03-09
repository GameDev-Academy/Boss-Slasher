using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class ScreenUnloadedEvent : EventBase
    {
        public ScreenData ScreenData { get; private set; }

        public static ScreenUnloadedEvent Create(ScreenData screen)
        {
            var eventData = EventFactory.Create<ScreenUnloadedEvent>();
            eventData.ScreenData = screen;
            return eventData;
        }
    }
}
