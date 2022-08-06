using UnityEngine;

namespace Enemy
{
    public interface IDamageDealer
    {
        int Damage { get; }
        bool Hit(out Collider hit);
    }
}