using System;
using UniRx;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public IReadOnlyReactiveProperty<bool> IsDead => _isDead;

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

        [SerializeField] private BoolReactiveProperty _isDead;

        private int _maxHealth;
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