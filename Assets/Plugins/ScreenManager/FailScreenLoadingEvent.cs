using ScreenManager.Enums;
using SimpleEventBus.Events;

namespace ScreenManager
{
    public class FailScreenLoadingEvent : EventBase
    {
        public FailScreenLoadingEvent(ScreenId type)
        {
            Type = type;
        }

        public ScreenId Type;
    }
}