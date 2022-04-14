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
        private WeaponsView _weaponsView;

        public void Initialize(
            IWeaponsSettingsProvider weaponsSettingsProvider, 
            IWeaponsService weaponsService, 
            IMoneyService moneyService)
        {
            _userMoneyPresenter.Initialize(moneyService);
            _weaponsView.Initialize(weaponsSettingsProvider, weaponsService, moneyService);
        }
    }
}