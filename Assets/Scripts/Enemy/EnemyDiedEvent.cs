using SimpleEventBus.Events;

namespace Enemy
{
    public class EnemyDiedEvent : EventBase
    {
        public Enemy Enemy { get; }


        public EnemyDiedEvent(Enemy enemy)
        {
            Enemy = enemy;
        }
    }
}