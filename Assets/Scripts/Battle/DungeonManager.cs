using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Battle
{
    public class DungeonManager : MonoBehaviour
    {
        [SerializeField] private List<Level> _levels;
        [SerializeField] private Portal _portal;

        private void Start()
        {
            var nextLevel = 1;

            foreach (var level in _levels)
            {
                level.IsPassed
                    .Where(isPassed => isPassed == true)
                    .Subscribe(_ =>
                    {
                        if (nextLevel != _levels.Count)
                        {
                            _levels[nextLevel].OpenLevel();
                            nextLevel++;
                        }
                    })
                    .AddTo(this);
            }

            EnablePortalWhenLastLevelPassed();
        }

        private void EnablePortalWhenLastLevelPassed()
        {
            _levels[^1].IsPassed
                .Where(_ => _)
                .Subscribe(_ => _portal.gameObject.SetActive(true))
                .AddTo(this);
        }
    }
}