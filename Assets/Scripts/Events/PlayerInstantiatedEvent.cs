using SimpleEventBus.Events;

namespace Events
{
    public class PlayerInstantiatedEvent : EventBase
    {
        public Player.Player Player;

        public PlayerInstantiatedEvent(Player.Player player)
        {
            Player = player;
        }
    }
}