using UnityEngine;

namespace BattleLoot
{
    /// <summary>
    /// The class is responsible for spawn loot from loot box
    /// </summary>
    public class SpawnAction : LootAction
    {
        [SerializeField] private GameObject _lootPrefab;
        public override void Execute()
        {
            Spawn();
        }

        private void Spawn()
        {
            Instantiate(_lootPrefab, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}