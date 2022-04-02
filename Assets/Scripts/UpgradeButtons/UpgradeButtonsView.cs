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
        //[SerializeField] 
        //private UpgradeButtonPresenter _buttonPresenter;
//
        //[SerializeField] 
        //private RectTransform _buttonsRoot;

        [SerializeField] 
        private List<UpgradeButtonPresenter> _upgradeButtons;

        [SerializeField] 
        private ConfigurationProvider _configurationProvider;

        public void Initialize(ICharacteristicsService characteristicsService, IMoneyService moneyService)
        {
            var characteristics =
                _configurationProvider.CharacteristicsSettingsProvider.GetAllCharacteristicsSettings();
            foreach (var upgradeButton in _upgradeButtons)
            {
                upgradeButton.Initialize(_configurationProvider, characteristicsService, moneyService);
            }
        }
    }
}