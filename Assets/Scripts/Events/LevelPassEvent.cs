using SimpleEventBus.Events;

namespace Events
{
    public class LevelPassEvent : EventBase
    {
        public bool IsLevelPassed { get; }

        public LevelPassEvent(bool isLevelPassed)
        {
            IsLevelPassed = isLevelPassed;
        }
    }
}