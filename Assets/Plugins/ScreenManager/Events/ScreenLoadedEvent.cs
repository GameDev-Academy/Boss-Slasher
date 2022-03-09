using ScreenManager.Enums;
using ScreenManager.Interfaces;
using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class ScreenLoadedEvent : EventBase
    {
        public IScreen Screen { get; private set; }
        // it's possible to have one screen with different ids
        public ScreenId Id { get; private set; }

        public static ScreenLoadedEvent Create(IScreen screen, ScreenId screenId)
        {
            var eventData = EventFactory.Create<ScreenLoadedEvent>();
            eventData.Screen = screen;
            eventData.Id = screenId;
            return eventData;
        }
    }
}
