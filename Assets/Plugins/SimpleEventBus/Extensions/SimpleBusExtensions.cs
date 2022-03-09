using SimpleEventBus.Events;
using SimpleEventBus.Interfaces;

namespace SimpleBus.Extensions
{
    public static class SimpleBusExtensions
    {
        public static void Publish(this EventBase eventBase, IEventBus eventBus)
        {
            eventBus.Publish(eventBase);
        }
    }
}
