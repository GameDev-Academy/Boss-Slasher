using Player;
using UnityEngine;

namespace Enemy
{
    public class AttackAnimationEvents : MonoBehaviour
    {
        private IDamageDealer _damageDealer;

        public void Initialize(IDamageDealer damageDealer)
        {
            _damageDealer = damageDealer;
        }

        private void OnAttack()
        {
            if (_damageDealer.Hit(out var hit))
            {
                hit.GetComponent<PlayerHealth>().TakeDamage(_damageDealer.Damage);
            }
        }   
    }
}