using System.Collections.Generic;
using ConfigurationProviders;
using UniRx;
using UnityEngine.Assertions;

namespace User
{
    public class CharacteristicsService : ICharacteristicsService
    {
        private const int INITIAL_CHARACTERISTIC_LEVEL = 1;
        
        private IConfigurationProvider _configurationProvider;
        private IMoneyService _moneyService;

        private Dictionary<CharacteristicType, ReactiveProperty<int>> _characteristicsLevels;

        public CharacteristicsService(IConfigurationProvider configurationProvider, IMoneyService moneyService)
        {
            _configurationProvider = configurationProvider;
            _moneyService = moneyService;
            _characteristicsLevels = new ();
        
            var characteristicsSettingsProvider = configurationProvider.CharacteristicsSettingsProvider;
            var characteristicsSettings = characteristicsSettingsProvider.GetAllCharacteristicsSettings();
            foreach (var characteristicSetting in characteristicsSettings)
            {
                //уровни характеристик тут будем заполнять из сохраненных настроек
                _characteristicsLevels[characteristicSetting.Type] = new ReactiveProperty<int>(INITIAL_CHARACTERISTIC_LEVEL);
            }
        }
    
        public IReadOnlyReactiveProperty<int> GetCharacteristicLevel(CharacteristicType type)
        {
            return _characteristicsLevels[type];
        }

        public void UpgradeCharacteristic(CharacteristicType type)
        {
            var characteristicsSettingsProvider = _configurationProvider.CharacteristicsSettingsProvider;
            var characteristicLevel = _characteristicsLevels[type];
            Assert.IsNotNull(characteristicLevel, $"CharacteristicLevel is null, please check the levels of {type} in " +
                                                  "characteristicsSettingsProvider");

            var upgradeCost =
                characteristicsSettingsProvider.GetUpgradeCostByLevel(type, characteristicLevel.Value);
            
            if (!CanUpgradeCharacteristic(type, characteristicLevel.Value, upgradeCost))
            {
                return;
            }

            _moneyService.Pay(upgradeCost);
            characteristicLevel.Value++;
        }

        private bool CanUpgradeCharacteristic(CharacteristicType characteristicType, int characteristicLevel,
            int upgradeCost)
        {
            var characteristicsSettingsProvider = _configurationProvider.CharacteristicsSettingsProvider;
            var isLastCharacteristicLevel =
                characteristicsSettingsProvider.IsLastCharacteristicLevel(characteristicType, characteristicLevel);
            
            return !isLastCharacteristicLevel && _moneyService.HasEnoughMoney(upgradeCost);
        }
    }
}