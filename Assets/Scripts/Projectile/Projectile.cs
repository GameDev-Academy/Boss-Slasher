using UnityEngine;

namespace Projectile
{
    public class Projectile : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            var health = other.gameObject.GetComponent<HealthBehaviour>();
            
            if (health != null)
            {
                health.TakeDamage(1);
            }
        }
    }
}