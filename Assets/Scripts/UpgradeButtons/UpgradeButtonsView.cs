using System.Collections.Generic;
using ConfigurationProviders;
using UnityEngine;
using User;

namespace UpgradeButtons
{
    /// <summary>
    /// Инициализирует кнопки апгрейда характеристик.
    /// </summary>
    public class UpgradeButtonsView : MonoBehaviour
    {
        [SerializeField] 
        private List<UpgradeButtonPresenter> _upgradeButtons;

        public void Initialize(
            IConfigurationProvider configurationProvider, 
            ICharacteristicsService characteristicsService, 
            IMoneyService moneyService)
        {
            foreach (var upgradeButton in _upgradeButtons)
            {
                upgradeButton.Initialize(configurationProvider, characteristicsService, moneyService);
            }
        }
    }
}