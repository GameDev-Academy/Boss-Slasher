using SimpleEventBus.Events;

namespace Events
{
    public class PlayerInstantiatedEvent : EventBase
    {
        public Player Player;

        public PlayerInstantiatedEvent(Player player)
        {
            Player = player;
        }
    }
}