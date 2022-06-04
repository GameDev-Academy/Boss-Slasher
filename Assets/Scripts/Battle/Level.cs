using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    public class Level : MonoBehaviour
    {
        public bool LevelPassed { get; private set; }
        public List<Room> Rooms => _rooms;

        [SerializeField] private List<Room> _rooms;

        private void Start()
        {
            LevelPassed = false;
        }

        private void FixedUpdate()
        {
            if (_rooms.All(room => room.IsPassed))
            {
                LevelPassed = true;
            }
        }
    }
}