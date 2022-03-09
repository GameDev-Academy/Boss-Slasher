using MicroRx.Core;
using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class GetScreenManagerStateEvent : EventBase
    {
        public IObservableSubject<bool> IsInProgress;
    }
}
