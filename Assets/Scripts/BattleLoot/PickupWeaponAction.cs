using UnityEngine;
using User;

namespace BattleLoot
{
    /// <summary>
    /// The class is responsible for picking up weapon in dungeon
    /// </summary>
    public class PickupWeaponAction : LootAction
    {
        [SerializeField] private string _weaponId;

        private IWeaponsService _weaponsService;

        private void Awake()
        {
            _weaponsService = ServiceLocator.Instance.GetSingle<IWeaponsService>();
        }
        
        public override void Execute()
        {
            PickupWeapon();
        }

        private void PickupWeapon()
        {
            _weaponsService.SelectAsMainWeapon(_weaponId); //todo оружие остается после битвы, добавить сервис? 
        }
        
    }
}