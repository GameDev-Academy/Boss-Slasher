using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Battle
{
    public class Level : MonoBehaviour
    {
        public IReadOnlyReactiveProperty<bool> IsPassed
        {
            get;
            private set;
        }

        [SerializeField] 
        private List<Room> _rooms;
        
        public void Initialize()
        {
            foreach (var room in _rooms)
            {
                room.Initialize();
            }

            IsPassed = AreAllRoomsPassed();
        }
        
        private IReadOnlyReactiveProperty<bool> AreAllRoomsPassed()
        {
            var roomsPassedProperties = _rooms
                .Select(room => room.IsPassed);
            
            return roomsPassedProperties
                .CombineLatest()
                .Select(roomsStates => roomsStates.All(isPassed => isPassed))
                .ToReactiveProperty();
        }

        public void OpenDoors()
        {
            foreach (var room in _rooms)
            {
                room.OpenDoor();
            }
        }
    }
}