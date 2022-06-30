using SimpleEventBus.Events;

namespace Events
{
    public class DungeonPassEvent : EventBase
    {
        public bool IsLevelPassed { get; }

        public DungeonPassEvent(bool isLevelPassed)
        {
            IsLevelPassed = isLevelPassed;
        }
    }
}