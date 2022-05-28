using System.Collections;
using Animators;
using UnityEngine;

namespace DealAndTakeDamage
{   
    /// <summary>
    /// Отвечает за кулдаун, передачу количества урона ДэмджДилеру, поворот
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
        private int _damageValue;
        private bool _stop;
        
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
            
            Debug.Log("StartAttack");
            DoRotationToTarget(collider);
            _animatorController.Hit();
        }

        private void DoRotationToTarget(Collider collider)
        {
            var targetPosition = collider.transform.position;
            var position = transform.position;
            var relativePosition = new Vector3(targetPosition.x - position.x,0f, targetPosition.z - position.z);
            var targetRotation = Quaternion.LookRotation(relativePosition);

            transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation, 1f);
        }
        
        //Вызов в определенной точке анимации
        private void StartDealDamage()
        {
            Debug.Log("StartDealDamage");
            _damageDealer.StartCollision();
            _isCooldown = true;
            StartCoroutine(Cooldown());
        }
        
        private void StopDealDamage()
        {
            Debug.Log("StopDealDamage");
            _damageDealer.StopCollision();
        }
        
        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(_attackInterval);
            _isCooldown = false;
        }

        public void Initialize(int value)
        {
            _damageValue = value;
            _damageDealer.SetDamageValue(_damageValue);
        }
    }
}