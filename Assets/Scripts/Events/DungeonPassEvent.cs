using SimpleEventBus.Events;
using UnityEngine;

namespace Events
{
    public class DungeonPassEvent : EventBase
    {
        public bool IsDungeonPassed { get; }

        public DungeonPassEvent(bool isDungeonPassed)
        {
            IsDungeonPassed = isDungeonPassed;
        }
    }
}