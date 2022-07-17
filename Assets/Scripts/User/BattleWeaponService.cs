namespace User
{
    public class BattleWeaponService : IBattleWeaponService
    {
        private readonly IWeaponsService _weaponService;
        private readonly string _userWeaponId;

        public BattleWeaponService(IWeaponsService weaponService)
        {
            _weaponService = weaponService;
            _userWeaponId = _weaponService.CurrentWeapon.Value;
        }
        
        public void SelectTemporaryWeapon(string id)
        {
            _weaponService.SelectWeapon(id);
        }

        public void ResetWeapon()
        {
            _weaponService.SelectWeapon(_userWeaponId);
        }
    }
}