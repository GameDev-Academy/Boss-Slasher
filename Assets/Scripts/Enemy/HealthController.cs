using System;
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

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
        }

        //todo сделать логику смерти Enemy
        public void KillEnemy()
        {
            EventStreams.UserInterface.Publish(new EnemyDiedEvent (_enemy));
        }
    }
}