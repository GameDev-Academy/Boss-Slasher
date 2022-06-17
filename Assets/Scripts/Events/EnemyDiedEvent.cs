using SimpleEventBus.Events;

namespace Events
{
    public class EnemyDiedEvent : EventBase
    {
        public Enemy.Enemy Enemy { get; }
        
        public EnemyDiedEvent(Enemy.Enemy enemy)
        {
            Enemy = enemy;
        }
    }
}