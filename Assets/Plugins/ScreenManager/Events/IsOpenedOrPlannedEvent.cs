using ScreenManager.Enums;
using SimpleBus.Extensions;
using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class IsOpenedOrPlannedEvent : EventBase
    {
        public bool Result;
        public ScreenId ScreenId;

        public static bool Check(ScreenId id)
        {
            var isOpenedOrPlannedEvent = EventFactory.Create<IsOpenedOrPlannedEvent>();
            isOpenedOrPlannedEvent.Result = false;
            isOpenedOrPlannedEvent.ScreenId = id;
            isOpenedOrPlannedEvent.Publish(EventStreams.UserInterface);
            return isOpenedOrPlannedEvent.Result;
        }
    }
}