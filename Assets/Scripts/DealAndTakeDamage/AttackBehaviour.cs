using System.Collections;
using UnityEngine;

namespace DealAndTakeDamage
{   
    /// <summary>
    /// Отвечает за кулдаун, передачу количества урона ДэмджДилеру
    /// </summary>
    public class AttackBehaviour : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _maskToAttack;
        [SerializeField]
        private float _attackInterval;
        
        private IDamageDealer _damageDealer;
        private IAnimatorController _animatorController;
        private bool _isCooldown;

        private void Awake()
        {
            _damageDealer = GetComponentInChildren<IDamageDealer>();
            _animatorController = GetComponent<IAnimatorController>();
        }
        
        public void StartAttack(Collider collider)
        {
            if (_isCooldown)
            {
                return;
            }

            if ((_maskToAttack.value & 1 << collider.gameObject.layer) == 0)
            {
                return;
            }
            
            _animatorController.Hit();
        }

        //Вызов в определенной точке анимации
        private void StartDealDamage()
        {
            _damageDealer.StartCollision();
            _isCooldown = true;
            StartCoroutine(Cooldown());
        }
        
        private void StopDealDamage()
        {
            _damageDealer.StopCollision();
        }
        
        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(_attackInterval);
            _isCooldown = false;
        }

        public void Initialize(int value)
        {
            _damageDealer.SetDamageValue(value);
        }
    }
}