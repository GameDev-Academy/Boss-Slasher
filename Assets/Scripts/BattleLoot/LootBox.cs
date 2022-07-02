using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace BattleLoot
{
    /// <summary>
    /// The class is responsible for open box
    /// </summary>
    public class LootBox : MonoBehaviour
    {
        private readonly int _isOpenedHash = Animator.StringToHash("IsOpened");

        [SerializeField] private Animator _animator;
        [SerializeField] private List<HealthHandler> _enemies;
        [SerializeField] private GameObject _loot;

        private IReadOnlyReactiveProperty<bool> _isOpened;

        private void Start()
        {
            _isOpened = AllEnemyDied();

            _isOpened.SubscribeWhenTrue(SpawnLoot);
        }

        private IReadOnlyReactiveProperty<bool> AllEnemyDied()
        {
            return _enemies.Select(enemy => enemy.IsDead)
                .CombineLatest()
                .Select(isDeadProperty => isDeadProperty.All(isDead => isDead))
                .ToReactiveProperty();
        }

        private void SpawnLoot()
        {
            OpenChest();
            _loot.SetActive(true);
        }

        private void OpenChest()
        {
            _animator.SetBool(_isOpenedHash, true);
        }
    }
}