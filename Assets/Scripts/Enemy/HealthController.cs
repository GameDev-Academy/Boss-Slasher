using Events;
using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Контролирует получение урона Enemy
    /// </summary>
    public class HealthController : MonoBehaviour
    {
        private Enemy _enemy;
        
        //todo сделать логику смерти Enemy
        public void KillEnemy()
        {
            EventStreams.UserInterface.Publish(new EnemyDiedEvent (_enemy));
        }
    }
}