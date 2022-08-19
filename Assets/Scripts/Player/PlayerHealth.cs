using System;
using UniRx;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        public Action HealthChanged;

        public IReadOnlyReactiveProperty<bool> IsDead => _isDead;

        public int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                HealthChanged?.Invoke();
            }
        }

        [SerializeField] private BoolReactiveProperty _isDead;

        private int _maxHealth;
        private int _currentHealth;
        
        
        public void InitializePlayerHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = _maxHealth;
            HealthChanged?.Invoke();
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