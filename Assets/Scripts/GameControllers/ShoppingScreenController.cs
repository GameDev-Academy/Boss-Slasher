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
        private WeaponShopPresenter _weaponShopPresenter;

        private void Awake()
        {
            var weaponsSettingsProvider = ServiceLocator.Instance.GetSingle<IWeaponsSettingsProvider>();
            var weaponsService = ServiceLocator.Instance.GetSingle<IWeaponsService>();
            var moneyService = ServiceLocator.Instance.GetSingle<IMoneyService>();
            _weaponShopPresenter.Initialize(weaponsSettingsProvider, weaponsService, moneyService);
        }
        
        [UsedImplicitly]
        public void CloseShop()
        {
            EventStreams.UserInterface.Publish(new CloseShopEvent());
        }
    }
}