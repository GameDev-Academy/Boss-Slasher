using System;
using Player;
using UnityEngine;

namespace Enemy
{
    public class AttackAnimationEvents : MonoBehaviour
    {
        private IDamageDealer _damageDealer;

        private void Awake()
        {
            _damageDealer = GetComponent<IDamageDealer>();
        }

        private void OnAttack()
        {
            if (_damageDealer.Hit(out var hit))
            {
                hit.GetComponent<IHealth>().TakeDamage(_damageDealer.Damage);
            }
        }   
    }
}