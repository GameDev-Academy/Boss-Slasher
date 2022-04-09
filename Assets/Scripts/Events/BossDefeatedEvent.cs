using SimpleEventBus.Events;

namespace Events
{
    public class BossDefeatedEvent : EventBase
    {
        public bool BossDefeaded => true;
    }
}