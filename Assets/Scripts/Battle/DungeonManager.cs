using System.Collections.Generic;
using UniRx;
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

            foreach (var level in _levels)
            {
                level.IsPassed
                    .Where(_ => _)
                    .Subscribe(_ => OpenNextLevel())
                    .AddTo(this);
            }

            SubscribeOnEnablePortal();
        }

        private void SubscribeOnEnablePortal()
        {
            _levels[^1].IsPassed
                .Where(_ => _)
                .Subscribe(_ => _portal.gameObject.SetActive(true))
                .AddTo(this);
        }

        private void OpenNextLevel()
        {
            _currentLevel++;

            if (_currentLevel != _levels.Count)
            {
                _levels[_currentLevel].OpenLevel();
            }
        }
    }
}