using Events;
using JetBrains.Annotations;
using UnityEngine;
using UpgradeButtons;
using User;
using WeaponsSettings;
using WeaponsShop;

namespace GameControllers
{
    public class ShoppingScreenController : MonoBehaviour
    {
        [SerializeField] 
        private UserMoneyPresenter _userMoneyPresenter;

        [SerializeField] 
        private WeaponShopPresenter _weaponShopPresenter;

        public void Initialize(
            IWeaponsSettingsProvider weaponsSettingsProvider, 
            IWeaponsService weaponsService, 
            IMoneyService moneyService)
        {
            _userMoneyPresenter.Initialize(moneyService);
            _weaponShopPresenter.Initialize(weaponsSettingsProvider, weaponsService, moneyService);
        }
        
        [UsedImplicitly]
        public void CloseShop()
        {
            EventStreams.UserInterface.Publish(new CloseShopEvent());
        }
    }
}