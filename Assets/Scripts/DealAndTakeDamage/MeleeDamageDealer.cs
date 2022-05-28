using UnityEngine;

namespace DealAndTakeDamage
{
    /// <summary>
    /// Проверят коллизии и вызывает у HealthBehaviour TakeDamage, прокинутого от AttackBehavior
    /// </summary>
    public class MeleeDamageDealer : MonoBehaviour, IDamageDealer
    {
        [SerializeField]
        private LayerMask _mask;

        private bool _isStartCollision;
        private int _damageValue;

        public void SetDamageValue(int damageValue)
        { 
            _damageValue = damageValue;
        }

        public void StartCollision()
        {
            _isStartCollision = true;
        }
        
        public void StopCollision()
        {
            _isStartCollision = false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_isStartCollision)
            {
                return;
            }
            
            if ((_mask.value & 1 << other.gameObject.layer) != 0)
            {
                return;
            }
            
            var health = other.gameObject.GetComponent<HealthBehaviour>();
            if (health != null)
            {
                health.TakeDamage(_damageValue);
            }
        }
    }
}