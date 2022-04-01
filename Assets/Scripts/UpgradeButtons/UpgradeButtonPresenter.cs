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
    /// Презентер отдельной кнопки апгрейда, отвечает за отображение на UI картинки характеристики (при инициализации),
    /// стоимость апгрейда характеристики, интерактивность кнопки (если достигнут мах уровень характеристики,
    /// либо у пользователя недостаточно денег для апгрейда - делает кнопку неинтерактивной).
    /// При нажатии на кнопку апгрейдит соответствующую характеристику
    /// </summary>
    public class UpgradeButtonPresenter : MonoBehaviour
    {
        [SerializeField] 
        private Button _upgradeCharacteristicButton;

        [SerializeField] 
        private Image _characteristicIcon;

        [SerializeField] 
        private TextMeshProUGUI _upgradeCost;

        private CharacteristicType _characteristicType;

        private ICharacteristicsService _characteristicsService;
        private IMoneyService _moneyService;

        private ReactiveProperty<bool> _isUserHasMoneyToUpgrade;
        private ReactiveProperty<bool> _notLastCharacteristicLevel;

        public void Initialize(IConfigurationProvider configurationProvider, CharacteristicType characteristicType,
            ICharacteristicsService characteristicsService, IMoneyService moneyService)
        {
            _characteristicType = characteristicType;
            _characteristicsService = characteristicsService;
            _moneyService = moneyService;

            var characteristicsSettingsProvider = configurationProvider.CharacteristicsSettingsProvider;
            var userMoney = _moneyService.Money;
            var level = _characteristicsService.GetCharacteristicLevel(_characteristicType);

            _characteristicIcon.sprite =
                characteristicsSettingsProvider.GetCharacteristicSettingsByType(_characteristicType).Icon;

            _isUserHasMoneyToUpgrade = new ReactiveProperty<bool>(true);
            _notLastCharacteristicLevel = new ReactiveProperty<bool>(true);

            //подписка на значения полей _isUserHasMoneyToUpgrade и _notLastCharacteristicLevel интерактивностью кнопки
            _isUserHasMoneyToUpgrade.SubscribeToInteractable(_upgradeCharacteristicButton);
            _notLastCharacteristicLevel.SubscribeToInteractable(_upgradeCharacteristicButton);

            //подписка на увеличение уровня характеристики (на случай, если характеристика будет увеличена не за деньги)
            level.ObserveEveryValueChanged(characteristicLevel => characteristicLevel.Value)
                .Subscribe(_ => CheckIfLevelHasMaxValue(characteristicsSettingsProvider)).AddTo(this);
            
            //подписка на изменение денег пользователя изменением поля _isUserHasMoneyToUpgrade
            userMoney.ObserveEveryValueChanged(money => money.Value)
                .Subscribe(_ => CheckIfUserHasMoneyToUpgrade(characteristicsSettingsProvider, userMoney.Value)).AddTo(this);

            //нажатие на кнопку - увеличение уровня
            _upgradeCharacteristicButton.OnClickAsObservable().Subscribe(_ => UpgradeCharacteristicLevel()).AddTo(this);
        }

        private void UpgradeCharacteristicLevel()
        {
            _characteristicsService.UpgradeCharacteristic(_characteristicType);
        }

        private void CheckIfUserHasMoneyToUpgrade(CharacteristicsSettingsProvider characteristicsSettingsProvider, int userMoney)
        {
            var currentLevel = _characteristicsService.GetCharacteristicLevel(_characteristicType);

            if (_notLastCharacteristicLevel.Value)
            {
                var upgradeCost =
                    characteristicsSettingsProvider.GetUpgradeCostByLevel(_characteristicType, currentLevel.Value);

                _upgradeCost.text = upgradeCost.ToString();
                _isUserHasMoneyToUpgrade.Value = userMoney >= upgradeCost;
                return;
            }
            _upgradeCost.text = "MAX";
        }

        private void CheckIfLevelHasMaxValue(CharacteristicsSettingsProvider characteristicsSettingsProvider)
        {
            var currentLevel = _characteristicsService.GetCharacteristicLevel(_characteristicType);

            if (!characteristicsSettingsProvider.IsLastCharacteristicLevel(_characteristicType, currentLevel.Value))
            {
                return;
            }
            _notLastCharacteristicLevel.Value = false;
        }
    }
}