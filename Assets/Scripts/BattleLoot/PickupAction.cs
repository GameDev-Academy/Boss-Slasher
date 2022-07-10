using UnityEngine;

namespace BattleLoot
{
    /// <summary>
    /// The class is responsible for picking up weapon in dungeon
    /// </summary>
    public class PickupAction : LootAction
    {
        [SerializeField] private GameObject _lootPrefab;
        public override void Execute(Collider collider)
        {
            WeaponPickup(collider);
        }
        
        private void WeaponPickup(Collider collider)
        {
            var weaponRoot = collider.gameObject.GetComponentInChildren<WeaponRoot>().transform;

            for (int i = 0; i < weaponRoot.childCount; i++)
            {
                Destroy(weaponRoot.GetChild(i).gameObject);
            }

            var weapon = Instantiate(_lootPrefab, weaponRoot);
            weapon.gameObject.transform.localPosition = Vector3.zero;
            weapon.gameObject.transform.localRotation = Quaternion.identity;
        }
    }
}