using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    public class DungeonManager : MonoBehaviour
    {
        [SerializeField] private List<Level> _levels;
        [SerializeField] private Portal _portal; 
        
        private int _currentLevel;
        
        private void Start()
        {
            _currentLevel = 0;
        }

        private void FixedUpdate()
        {
            if (_levels.Count == _currentLevel)
            {
                return;
            }
            
            if (_levels[_currentLevel].LevelPassed)
            {
                _currentLevel++;
                
                OpenNextLevel(_currentLevel);
            }

            if (_levels.All(level => level.LevelPassed))
            {
                _portal.gameObject.SetActive(true);
            }
        }

        private void OpenNextLevel(int currentLevel)
        {
            if (_levels.Count == currentLevel)
            {
                return;
            }
            
            foreach (var room in _levels[currentLevel].Rooms)
            {
                room.Door.IsOpen(true);
            }
        }
    }
}