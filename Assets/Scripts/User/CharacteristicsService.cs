using CharacteristicsSettings;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace User
{
    public class CharacteristicsService : ICharacteristicsService
    {
        private readonly ICharacteristicsSettingsProvider _characteristicsSettings;
        private readonly IMoneyService _moneyService;
        private readonly ICharacteristicsProvider _characteristicsProvider;

        public CharacteristicsService(ICharacteristicsProvider characteristicsProvider, 
            ICharacteristicsSettingsProvider characteristicsSettings,
            IMoneyService moneyService)
        {
            _characteristicsSettings = characteristicsSettings;
            _moneyService = moneyService;
            _characteristicsProvider = characteristicsProvider;
        }

        public IReadOnlyReactiveProperty<int> GetCharacteristicLevel(CharacteristicType type)
        {
            return _characteristicsProvider.CharacteristicsLevels[type];
        }

        public void UpgradeCharacteristic(CharacteristicType type)
        {
            var characteristicLevel = _characteristicsProvider.CharacteristicsLevels[type];
            Assert.IsNotNull(characteristicLevel, $"CharacteristicLevel is null, please check the levels of {type} in " +
                                                  "characteristicsSettingsProvider");

            if (!CanUpgradeCharacteristic(type, characteristicLevel.Value))
            {
                Debug.LogError($"Can't upgrade the characterstic with type {type}");
                return;
            }

            var upgradeCost = _characteristicsSettings.GetUpgradeCost(type, characteristicLevel.Value);
            _moneyService.Pay(upgradeCost);
            characteristicLevel.Value++;
        }

        private bool CanUpgradeCharacteristic(CharacteristicType characteristicType, int characteristicLevel)
        {
            var upgradeCost = _characteristicsSettings.GetUpgradeCost(characteristicType, characteristicLevel);
            var isLastLevel = _characteristicsSettings.IsLastLevel(characteristicType, characteristicLevel);

            return !isLastLevel && _moneyService.HasEnoughMoney(upgradeCost);
        }
    }
}