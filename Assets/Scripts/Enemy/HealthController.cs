using System;
using UniRx;
using UnityEngine;

namespace Enemy
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        private CompositeDisposable _subscriptions;

        
        public void KillEnemy()
        {
            EventStreams.UserInterface.Publish(new EnemyDiedEvent (_enemy));
        }
    }
}