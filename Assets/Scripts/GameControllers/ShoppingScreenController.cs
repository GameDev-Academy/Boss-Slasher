using UnityEngine;
using UpgradeButtons;
using User;
using WeaponsSettings;

namespace GameControllers
{
    public class ShoppingScreenController : MonoBehaviour
    {
        [SerializeField] 
        private UserMoneyPresenter _userMoneyPresenter;

        public void Initialize(IWeaponsSettingsProvider weaponsSettingsProvider,
            IMoneyService moneyService)
        {
            _userMoneyPresenter.Initialize(moneyService);
        }
    }
}