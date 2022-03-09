using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class ScreenHiddenEvent : EventBase
    {
        public ScreenData ScreenData { get; private set; }

        public static ScreenHiddenEvent Create(ScreenData screen)
        {
            var eventData = EventFactory.Create<ScreenHiddenEvent>();
            eventData.ScreenData = screen;
            return eventData;
        }
    }
}