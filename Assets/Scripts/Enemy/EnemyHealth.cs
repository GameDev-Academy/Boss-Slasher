using System;
using UniRx;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        public int CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        public IReadOnlyReactiveProperty<bool> IsDead => _isDead;

        [SerializeField] private BoolReactiveProperty _isDead;
        [SerializeField] private int _maxHealth = 1;

        private int _currentHealth;


        private void Awake()
        {
            _isDead.Value = false;

            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                _isDead.Value = true;
            }
        }
    }
}