using Enemy;
using UnityEngine;

namespace Player
{
    public class PlayerAttackHandler : MonoBehaviour
    {
        [SerializeField] private float _attackCooldown = 1f;

        private ITargetProvider _targetProvider;
        private IDamageDealer _damageDealer;
        private PlayerAnimationController _playerAnimationController;
        private float _currentCooldown;
        private bool _isAttacking;


        private void Awake()
        {
            _targetProvider = GetComponent<ITargetProvider>();
        }

        public void Initialize(PlayerAnimationController playerAnimationController)
        {
            _playerAnimationController = playerAnimationController;
            _damageDealer = GetComponent<IDamageDealer>();
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
            {
                StartAttack();
            }
        }

        private void StartAttack()
        {
            var target = _targetProvider.GetNearestTarget();

            _isAttacking = true;

            if (target != null)
            {
                transform.LookAt(target.transform);
                _playerAnimationController.PlayHit();
            }
        }

        private void OnAttack()
        {
            if (_damageDealer.Hit(out var hit))
            {
                hit.GetComponent<IHealth>().TakeDamage(_damageDealer.Damage);
            }
        }

        private void OnAttackEnded()
        {
            _currentCooldown = _attackCooldown;
            _isAttacking = false;
        }

        private bool CanAttack()
        {
            return !_isAttacking && _currentCooldown <= 0 && _targetProvider.HasAnyTarget();
        }

        private void UpdateCooldown()
        {
            if (_currentCooldown > 0)
            {
                _currentCooldown -= Time.deltaTime;
            }
        }
    }
}