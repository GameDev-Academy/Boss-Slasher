using System;
using UnityEngine;

namespace Player
{
    public class PlayerHealthPresenter : MonoBehaviour

    {
        [SerializeField] private HealthBar _healthBar;

        private PlayerHealth _playerHealth;


        public void Initialize(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _healthBar.SetStartHealth(_playerHealth.CurrentHealth);
            _playerHealth.HealthChanged += UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            _healthBar.SetCurrentHealth(_playerHealth.CurrentHealth);
        }

        private void OnDestroy()
        {
            _playerHealth.HealthChanged -= UpdateHpBar;
        }
    }
}