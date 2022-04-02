using CharacteristicsSettings;
using ConfigurationProviders;
using JetBrains.Annotations;
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

        private ReactiveProperty<bool> _isUserHasMoneyToUpgrade;
        private ReactiveProperty<bool> _notLastCharacteristicLevel;

        public void Initialize(IConfigurationProvider configurationProvider, 
            ICharacteristicsService characteristicsService, IMoneyService moneyService)
        {
            _characteristicsService = characteristicsService;
            _moneyService = moneyService;

            var characteristicsSettingsProvider = configurationProvider.CharacteristicsSettingsProvider;
            var userMoney = _moneyService.Money;
            var level = _characteristicsService.GetCharacteristicLevel(_characteristicType);
            
            _isUserHasMoneyToUpgrade = new ReactiveProperty<bool>(true);
            _notLastCharacteristicLevel = new ReactiveProperty<bool>(true);

            //подписка на значения полей _isUserHasMoneyToUpgrade и _notLastCharacteristicLevel интерактивностью кнопки
            _isUserHasMoneyToUpgrade.SubscribeToInteractable(_upgradeCharacteristicButton);
            _notLastCharacteristicLevel.SubscribeToInteractable(_upgradeCharacteristicButton);

            //подписка на увеличение уровня характеристики
            level.Subscribe(_ => UpdateButtonView(characteristicsSettingsProvider, userMoney.Value)).AddTo(this);
            
            //нажатие на кнопку - увеличение уровня
            _upgradeCharacteristicButton.OnClickAsObservable().Subscribe(_ => UpgradeCharacteristicLevel()).AddTo(this);
        }

        private void UpgradeCharacteristicLevel()
        {
            _characteristicsService.UpgradeCharacteristic(_characteristicType);
        }

        private void UpdateButtonView(CharacteristicsSettingsProvider characteristicsSettingsProvider, int userMoney)
        {
            var currentLevel = _characteristicsService.GetCharacteristicLevel(_characteristicType);

            if (!characteristicsSettingsProvider.IsLastCharacteristicLevel(_characteristicType, currentLevel.Value))
            {
                var upgradeCost =
                    characteristicsSettingsProvider.GetUpgradeCostByLevel(_characteristicType, currentLevel.Value);

                _upgradeCost.text = upgradeCost.ToString();
                _isUserHasMoneyToUpgrade.Value = userMoney >= upgradeCost;
                return;
            }
            
            _notLastCharacteristicLevel.Value = false;
            _upgradeCost.text = MAX_LEVEL_BUTTON_TEXT;
        }
    }
}