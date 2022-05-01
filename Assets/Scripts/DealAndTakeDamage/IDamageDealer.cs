using UnityEngine;

namespace DealAndTakeDamage
{
    public interface IDamageDealer
    {
        void SetDamageValue(int damageValue);
        void StartCollision();
        void StopCollision();
    }
}