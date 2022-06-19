using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Battle
{
    public class Level : MonoBehaviour
    {
        public IReadOnlyReactiveProperty<bool> IsPassed => _isPassed;

        [SerializeField] private List<Room> _rooms;

        private ReactiveProperty<bool> _isPassed = new();


        private void Start()
        {
            _isPassed.Value = false;

            var isAllRoomsPassed = CombineAllRoomsIsPassed();

            isAllRoomsPassed
                .Where(_ => _)
                .Subscribe(_ => _isPassed.Value = true)
                .AddTo(this);
        }

        private IReadOnlyReactiveProperty<bool> CombineAllRoomsIsPassed()
        {
            return _rooms
                .Select(room => room.IsPassed)
                .CombineLatest()
                .Select(isPassedProperty => isPassedProperty.All(isPassed => isPassed == true))
                .ToReactiveProperty();
        }

        public void OpenLevel()
        {
            foreach (var room in _rooms)
            {
                room.OpenDoor();
            }
        }
    }
}