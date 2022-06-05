using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Unity.VisualScripting;

namespace Battle
{
    public class Room : MonoBehaviour
    {
        public bool IsPassed => _isPassed;
        public Door Door => _door;
        
        [SerializeField] private Door _door;
        [SerializeField] private List<HealthBehaviour> _enemies;
        
        private bool _isPassed;

        private void Start()
        {
            _door.ToggleStateOfDoor(false);
        }

        private void FixedUpdate()
        {
            if (_enemies.All(enemy => enemy.IsDead.Value))
            {
                _isPassed = true;
            }

            if (_isPassed)
            {
                _door.ToggleStateOfDoor(true);
            }
        }
    }
}