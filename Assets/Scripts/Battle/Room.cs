using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


namespace Battle
{
    public class Room : MonoBehaviour
    {
        public IReadOnlyReactiveProperty<bool> IsPassed => _isPassed;

        [SerializeField] private Door _door;
        [SerializeField] private List<HealthHandler> _enemies;

        private ReactiveProperty<bool> _isPassed = new();


        private void Start()
        {
            _isPassed.Value = false;
            CloseDoor();

            var allEnemyIsDead = CombineAllEnemyIsDead();

            allEnemyIsDead
                .Where(_ => _)
                .Subscribe(_ =>
                {
                    _isPassed.Value = true;
                    OpenDoor();
                })
                .AddTo(this);
        }

        public void OpenDoor()
        {
            _door.Open(true);
        }

        private void CloseDoor()
        {
            _door.Open(false);
        }

        private IReadOnlyReactiveProperty<bool> CombineAllEnemyIsDead()
        {
            return _enemies
                .Select(enemy => enemy.IsDead)
                .CombineLatest()
                .Select(value => value.All(_ => _))
                .ToReactiveProperty();
        }
    }
}