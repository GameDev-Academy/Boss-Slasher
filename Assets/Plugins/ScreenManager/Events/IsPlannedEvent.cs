using ScreenManager.Enums;
using SimpleBus.Extensions;
using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class IsPlannedEvent : EventBase
    {
        public bool Result;
        public ScreenId ScreenId;

        public static bool Check(ScreenId id)
        {
            var isPlannedEvent = EventFactory.Create<IsPlannedEvent>();
            isPlannedEvent.Result = false;
            isPlannedEvent.ScreenId = id;
            isPlannedEvent.Publish(EventStreams.UserInterface);
            return isPlannedEvent.Result;
        }
    }
}