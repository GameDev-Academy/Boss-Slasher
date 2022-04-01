using ConfigurationProviders;
using UnityEngine;
using User;

namespace UpgradeButtons
{
    /// <summary>
    /// Инстанциирует и инициализирует кнопки апгрейда характеристик.
    /// </summary>
    public class UpgradeButtonsView : MonoBehaviour
    {
        [SerializeField] 
        private UpgradeButtonPresenter _buttonPresenter;

        [SerializeField] 
        private RectTransform _buttonsRoot;

        [SerializeField] 
        private ConfigurationProvider _configurationProvider;

        public void Initialize(ICharacteristicsService characteristicsService, IMoneyService moneyService)
        {
            var characteristics =
                _configurationProvider.CharacteristicsSettingsProvider.GetAllCharacteristicsSettings();
            foreach (var characteristic in characteristics)
            {
                var characteristicUpgradeButton = Instantiate(_buttonPresenter, _buttonsRoot);
                
                characteristicUpgradeButton.Initialize(_configurationProvider, characteristic.Type,
                    characteristicsService, moneyService);
            }
        }
    }
}