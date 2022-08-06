using System.Collections.Generic;
using Events;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;


namespace Battle
{
    /// <summary>
    /// The class is responsible for the logic of the dungeon,
    /// the opening of new levels and the spawning of the portal.
    /// </summary>
    public class DungeonManager : MonoBehaviour
    {
        [SerializeField] private List<Level> _levels;
        [SerializeField] private Portal _portal;

        private void Start()
        {
            foreach (var level in _levels)
            {
                level.Initialize();
            }

            for (var i = 0; i < _levels.Count - 1; i++)
            {
                var level = _levels[i];
                var nextLevel = _levels[i + 1];

                level.IsPassed.SubscribeWhenTrue(() => nextLevel.OpenDoors());
            }

            EnablePortalWhenLastLevelPassed();

            _portal.IsDungeonPassed.SubscribeWhenTrue(() =>
            {
                EventStreams.UserInterface.Publish(new DungeonPassEvent(true));
            });
        }

        private void EnablePortalWhenLastLevelPassed()
        {
            _levels[^1].IsPassed.SubscribeWhenTrue(EnablePortal);
        }

        private void EnablePortal()
        {
            _portal.gameObject.SetActive(true);
        }
    }
}