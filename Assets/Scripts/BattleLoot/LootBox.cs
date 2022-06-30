using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace BattleLoot
{
    public class LootBox : MonoBehaviour
    {
        private readonly int _isOpenedHash = Animator.StringToHash("IsOpened");
        private Animator _animator;
        private IReadOnlyReactiveProperty<bool> _isOpened;

        [SerializeField] private List<HealthHandler> _enemies;
        [SerializeField] private GameObject _loot;


        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

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