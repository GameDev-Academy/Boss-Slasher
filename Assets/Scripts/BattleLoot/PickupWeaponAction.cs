using UnityEngine;
using User;

namespace BattleLoot
{
    /// <summary>
    /// The class is responsible for picking up weapon in dungeon
    /// </summary>
    public class PickupWeaponAction : LootAction
    {
        [SerializeField]
        private string _weaponId;

        private IBattleWeaponService _weaponsService;

        private void Awake()
        {
            _weaponsService = ServiceLocator.Instance.GetSingle<IBattleWeaponService>();
        }
        
        public override void Execute()
        {
            PickupWeapon();
        }

        private void PickupWeapon()
        {
            _weaponsService.SelectTemporaryWeapon(_weaponId);
        }
        
    }
}