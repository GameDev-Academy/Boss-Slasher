using UnityEngine;

namespace BattleLoot
{
    public abstract class LootAction : MonoBehaviour
    {
        public abstract void Execute(Collider collider);
    }
}