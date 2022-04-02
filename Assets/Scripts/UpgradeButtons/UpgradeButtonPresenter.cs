using CharacteristicsSettings;
using ConfigurationProviders;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using User;

namespace UpgradeButtons
{
    /// <summary>
    /// Презентер отдельной кнопки апгрейда.
    /// Контролирует состояние кнопки апгрейда характеристики.
    /// </summary>
    public class UpgradeButtonPresenter : MonoBehaviour
    {
        private const string MAX_LEVEL_BUTTON_TEXT = "MAX";

        [SerializeField] 
        private CharacteristicType _characteristicType;

        [SerializeField] 
        private Button _upgradeCharacteristicButton;

        [SerializeField] 
        private TextMeshProUGUI _upgradeCost;

        private ICharacteristicsService _characteristicsService;
        private IMoneyService _moneyService;

        public void Initialize(IConfigurationProvider configurationProvider,
            ICharacteristicsService characteristicsService, IMoneyService moneyService)
        {
            _characteristicsService = characteristicsService;
            _moneyService = moneyService;

            var characteristicsSettingsProvider = configurationProvider.CharacteristicsSettingsProvider;
            var userMoney = _moneyService.Money;
            var level = _characteristicsService.GetCharacteristicLevel(_characteristicType);

            _upgradeCharacteristicButton
                .OnClickAsObservable()
                .Subscribe(_ => UpgradeCharacteristicLevel())
                .AddTo(this);

            level
                .Subscribe(_ => UpdateButtonView(characteristicsSettingsProvider, level.Value))
                .AddTo(this);
            
            userMoney
                .Merge(level)
                .Select(_ => CanUpdate(characteristicsSettingsProvider, userMoney.Value, level.Value))
                .SubscribeToInteractable(_upgradeCharacteristicButton);
        }

        private void UpgradeCharacteristicLevel()
        {
            _characteristicsService.UpgradeCharacteristic(_characteristicType);
        }

        private bool CanUpdate(CharacteristicsSettingsProvider characteristicsSettingsProvider, int userMoney,
            int level)
        {
            var upgradeCost = characteristicsSettingsProvider.GetUpgradeCostByLevel(_characteristicType, level);
            var isEnoughMoney = userMoney >= upgradeCost;
            var isLastCharacteristicLevel = 
                characteristicsSettingsProvider.IsLastCharacteristicLevel(_characteristicType, level);

            return isEnoughMoney && !isLastCharacteristicLevel;
        }

        private void UpdateButtonView(CharacteristicsSettingsProvider characteristicsSettingsProvider, int level)
        {
            if (!characteristicsSettingsProvider.IsLastCharacteristicLevel(_characteristicType, level))
            {
                var upgradeCost =
                    characteristicsSettingsProvider.GetUpgradeCostByLevel(_characteristicType, level);

                _upgradeCost.text = upgradeCost.ToString();
                return;
            }

            _upgradeCost.text = MAX_LEVEL_BUTTON_TEXT;
        }
    }
}