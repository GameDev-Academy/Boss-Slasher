using UnityEngine;
using User;

namespace UpgradeButtons
{
    public class MetaGameController : MonoBehaviour
    {
        public void Initialize(ICharacteristicsService characteristicsService, IMoneyService moneyService)
        {
            var upgradeButtonsView = FindObjectOfType<UpgradeButtonsView>();
            var userMoneyPresenter =  FindObjectOfType<UserMoneyPresenter>();
            upgradeButtonsView.Initialize(characteristicsService, moneyService);
            userMoneyPresenter.Initialize(moneyService);
        }
    }
}