using WeaponsSettings;

namespace User
{
    public class WeaponsService : IWeaponsService
    {
        private IWeaponsSettingsProvider _weaponsSettingsProvider;
        private readonly IMoneyService _moneyService;
        private readonly IWeaponProvider _weaponProvider;
        
        public WeaponsService(
            IWeaponsSettingsProvider weaponsSettingsProvider,
            IWeaponProvider weaponProvider, 
            IMoneyService moneyService)
        {
            _weaponsSettingsProvider = weaponsSettingsProvider;
            _weaponProvider = weaponProvider;
            _moneyService = moneyService;
        }

        public void BuyWeapon(string id)
        {
            var weaponCost = _weaponsSettingsProvider.GetCost(id);
            if (!_moneyService.HasEnoughMoney(weaponCost))
            {
                return;
            }
            
            _moneyService.Pay(weaponCost);
            _weaponProvider.Weapons.Add(id);
        }
    
        public void SelectWeapon(string id)
        {
            _weaponProvider.CurrentWeapon.Value = id;
        }
    }
}