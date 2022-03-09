using ScreenManager.Enums;
using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class GetTopScreenEvent : EventBase
    {
        public ScreenId? TopScreen;
    }
}