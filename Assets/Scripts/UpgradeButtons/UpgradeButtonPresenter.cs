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
        private ICharacteristicsSettingsProvider _characteristicsSettings;

        public void Initialize(IConfigurationProvider configurationProvider,
            ICharacteristicsService characteristicsService, IMoneyService moneyService)
        {
            _characteristicsService = characteristicsService;
            _moneyService = moneyService;
            _characteristicsSettings = configurationProvider.CharacteristicsSettings;
            
            _upgradeCharacteristicButton
                .OnClickAsObservable()
                .Subscribe(_ => _characteristicsService.UpgradeCharacteristic(_characteristicType))
                .AddTo(this);
            
            var level = _characteristicsService.GetCharacteristicLevel(_characteristicType);
            level
                .Subscribe(currentLevel => UpdateButtonView(_characteristicsSettings, currentLevel))
                .AddTo(this);

            var hasEnoughMoney = _moneyService.Money
                .CombineLatest(level, HasEnoughMoneyToUpgrade);

            var isLastLevel = level
                .Select(currentLevel => _characteristicsSettings.IsLastLevel(_characteristicType, currentLevel));

            hasEnoughMoney.CombineLatest(isLastLevel, CanUpgrade)
                .SubscribeToInteractable(_upgradeCharacteristicButton).AddTo(this);
        }

        private bool CanUpgrade(bool hasEnoughMoney, bool isLastLevel)
        {
            return hasEnoughMoney && !isLastLevel;
        }
        
        private bool HasEnoughMoneyToUpgrade(int userMoney, int level)
        {
            var upgradeCost = _characteristicsSettings.GetUpgradeCost(_characteristicType, level);
            var isEnoughMoney = userMoney >= upgradeCost;
            return isEnoughMoney;
        }

        private void UpdateButtonView(ICharacteristicsSettingsProvider characteristicsSettingsProvider, int level)
        {
            if (!characteristicsSettingsProvider.IsLastLevel(_characteristicType, level))
            {
                var upgradeCost =
                    characteristicsSettingsProvider.GetUpgradeCost(_characteristicType, level);

                _upgradeCost.text = upgradeCost.ToString();
                return;
            }

            _upgradeCost.text = MAX_LEVEL_BUTTON_TEXT;
        }
    }
}