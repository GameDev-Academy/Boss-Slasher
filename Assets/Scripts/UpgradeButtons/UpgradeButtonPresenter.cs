using ConfigurationProviders;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using User;

namespace UpgradeButtons
{
    /// <summary>
    /// Презентер отдельной кнопки апгрейда
    /// </summary>
    public class UpgradeButtonPresenter : MonoBehaviour
    {
        [SerializeField]
        private Image _characteristicIcon;
    
        [SerializeField] 
        private TextMeshProUGUI _upgradeCost;
        
        [SerializeField] 
        private Button _upgradeCharacteristicButton;

        private CharacteristicType _characteristicType;
        
        private IConfigurationProvider _configurationProvider;
        private ICharacteristicsService _characteristicsService;
        private IMoneyService _moneyService;

        private ReactiveProperty<bool> _isUserHasEnoughMoneyToUpgrade;
        private ReactiveProperty<bool> _notLastCharacteristicLevel;

        public void Initialize(IConfigurationProvider configurationProvider, CharacteristicType characteristicType,
            ICharacteristicsService characteristicsService, IMoneyService moneyService)
        {
            _configurationProvider = configurationProvider;
            _characteristicsService = characteristicsService;
            _moneyService = moneyService;
            _characteristicType = characteristicType;
            
            var characteristicsSettingsProvider = _configurationProvider.CharacteristicsSettingsProvider;
            
            _characteristicIcon.sprite = characteristicsSettingsProvider.GetCharacteristicSettingsByType(characteristicType).Icon;
            
            var userMoney = _moneyService.Money;
            var currentLevel = _characteristicsService.GetCharacteristicLevel(_characteristicType).Value;

            _isUserHasEnoughMoneyToUpgrade = new ReactiveProperty<bool>(true);

            //подписка на значение поля _isUserHasEnoughMoneyToUpgrade интерактивностью кнопки
            //(при значении поля false - кнопка станет неинтерактивной)
            _isUserHasEnoughMoneyToUpgrade.ObserveEveryValueChanged(_ => _.Value)
                .SubscribeToInteractable(_upgradeCharacteristicButton);

            _notLastCharacteristicLevel = new ReactiveProperty<bool>(!
                characteristicsSettingsProvider.IsLastCharacteristicLevel(_characteristicType, currentLevel));

            //подписка на значение поля _notLastCharacteristicLevel интерактивностью кнопки
            //(при значении поля false - кнопка станет неинтерактивной)
            _notLastCharacteristicLevel.SubscribeToInteractable(_upgradeCharacteristicButton);

            //подписка на изменение денег пользователя изменением поля _isUserHasEnoughMoneyToUpgrade
            userMoney.ObserveEveryValueChanged(money => money.Value)
                .Subscribe(_ => ChangeButtonView(userMoney.Value)).AddTo(this);

            //подписка на увеличение уровня характеристики
            _characteristicsService.GetCharacteristicLevel(_characteristicType)
                .ObserveEveryValueChanged(characteristic => characteristic.Value)
                .Subscribe(_ => ChangeButtonView(userMoney.Value)).AddTo(this);
        
            //нажатие на кнопку - увеличение уровня
            _upgradeCharacteristicButton.OnClickAsObservable().Subscribe(_ => UpgradeLevel()).AddTo(this);
        }

        private void UpgradeLevel()
        {
            _characteristicsService.UpgradeCharacteristic(_characteristicType);
        }

        private void ChangeButtonView(int userMoney)
        {
            var characteristicsSettingsProvider = _configurationProvider.CharacteristicsSettingsProvider;
            var currentLevel = _characteristicsService.GetCharacteristicLevel(_characteristicType);

            //если текущий уровень не последний, получаем стоимость апгрейда
            if (!characteristicsSettingsProvider.IsLastCharacteristicLevel(_characteristicType, currentLevel.Value))
            {
                var upgradeCost =
                    characteristicsSettingsProvider.GetUpgradeCostByLevel(_characteristicType, currentLevel.Value);

                _upgradeCost.text = upgradeCost.ToString();
                _isUserHasEnoughMoneyToUpgrade.Value = userMoney >= upgradeCost;
                return;
            }

            //если текущий уровень последний
            _upgradeCost.text = "MAX";
            _notLastCharacteristicLevel.Value = false;
        }
    }
}