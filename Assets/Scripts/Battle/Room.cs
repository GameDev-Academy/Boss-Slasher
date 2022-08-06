using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


namespace Battle
{
    /// <summary>
    /// The class is responsible for opening the door in the room
    /// </summary>
    public class Room : MonoBehaviour
    {
        public IReadOnlyReactiveProperty<bool> IsPassed { get; private set; }

        [SerializeField] private Door _door;
        [SerializeField] private List<HealthHandler> _enemies;

        public void Initialize()
        {
            CloseDoor();

            IsPassed = AreAllEnemiesDied();

            // when the room is passed we should allow the player to leave the room
            IsPassed.SubscribeWhenTrue(OpenDoor);
        }

        public void OpenDoor()
        {
            _door.SetOpenedState(true);
        }

        private void CloseDoor()
        {
            _door.SetOpenedState(false);
        }

        private IReadOnlyReactiveProperty<bool> AreAllEnemiesDied()
        {
            return _enemies
                .Select(enemy => enemy.IsDead)
                .CombineLatest()
                .Select(isDeadProperties => isDeadProperties.All(isDead => isDead))
                .ToReactiveProperty();
        }
    }
}