using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    public class Level : MonoBehaviour
    {
        public bool IsLevelPassed { get; private set; }
        public List<Room> Rooms => _rooms;

        [SerializeField] private List<Room> _rooms;

        private void Start()
        {
            IsLevelPassed = false;
        }

        private void FixedUpdate()
        {
            if (_rooms.All(room => room.IsPassed))
            {
                IsLevelPassed = true;
            }
        }
    }
}