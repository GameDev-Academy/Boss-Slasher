using UnityEngine;
using User;

namespace UpgradeButtons
{
    /// <summary>
    /// MonoBehaviour, который при переходе в MetaGameState ищет на сцене UpgradeButtonsView и UserMoneyPresenter
    /// и инициализирует их сервисами
    /// </summary>
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