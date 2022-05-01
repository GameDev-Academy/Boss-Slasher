using UnityEngine;

namespace DealAndTakeDamage
{
    /// <summary>
    /// Проверят коллизии и вызывает у HealthBehaviour получение количество урона, прокинутого от AttackBehavior
    /// </summary>
    public class MeleeDamageDealer : MonoBehaviour, IDamageDealer
    {
        [SerializeField]
        private LayerMask _mask;

        private bool _isStartCollision;
        private int _damageValue = 1;

        public void SetDamageValue(int damageValue)
        {
            //_damageValue = damageValue;
        }

        public void StartCollision()
        {
            //Debug.Log("_isStartCollision = TRUE");
            _isStartCollision = true;
        }
        
        public void StopCollision()
        {
            //Debug.Log("_isStartCollision = FALSE");
            _isStartCollision = false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_isStartCollision)
            {
                //Debug.Log("!_isStartCollision");
                return;
            }
            
            if ((_mask.value & 1 << other.gameObject.layer) != 0)
            {
                //Debug.Log("DON'T hit your TEAM");
                return;
            }
            
            var health = other.gameObject.GetComponent<HealthBehaviour>();
            if (health != null)
            {
                //Debug.Log("TakeDamage: " + _damageValue);
                health.TakeDamage(_damageValue);
            }
        }
    }
}