using SimpleEventBus.Events;

namespace SimpleBus.Interfaces
{
    public interface ISubscription
    {
        /// <summary>
        /// Publish to the subscriber
        /// </summary>
        /// <param name="eventBase"></param>
        void Publish(EventBase eventBase);
    }
}
