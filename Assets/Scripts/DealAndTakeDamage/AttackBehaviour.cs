using System;
using System.Collections;
using Animators;
using BattleCharacteristics;
using UniRx;
using UnityEngine;

namespace DealAndTakeDamage
{   
    /// <summary>
    /// Отвечает за кулдаун, передачу количества урона ДэмджДилеру, поворот
    /// </summary>
    public class AttackBehaviour : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _mask;
        [SerializeField]
        private float _attackInterval;
        
        private IDamageDealer _damageDealer;
        private IAnimatorController _animatorController;
        private bool _isCooldown;
        private int _damageValue;
        private float _startAttackInterval;

        private void Awake()
        {
            _damageDealer = (IDamageDealer)GetComponentInChildren(typeof(IDamageDealer));
            _animatorController = (IAnimatorController)GetComponent(typeof(IAnimatorController));
            _damageDealer.SetDamageValue(_damageValue);
            _startAttackInterval = _attackInterval;
        }
        
        public void StartAttack(Collider collider)
        {
            if (_isCooldown || (_mask.value & 1 << collider.gameObject.layer) == 0)
            {
                return;
            }

            DoRotationToTarget(collider);
            _animatorController.Hit();
        }

        public void StopAttack(Collider collider)
        {
            //_animatorController.Run();
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
        private void OnDealDamage()
        {
            _damageDealer.StartCollision();
            //StartCooldown();
        }

        private void StartCooldown()
        {
            _damageDealer.StopCollision();
            _animatorController.Run();
            _startAttackInterval -= Time.deltaTime;
        }

        public void Initialize(int value)
        {
            _damageValue = value;
            Debug.Log("DAMAGE: " + value);
        }
    }
}