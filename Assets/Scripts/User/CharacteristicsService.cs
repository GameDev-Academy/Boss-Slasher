using System;
using System.Collections.Generic;
using System.Linq;
using CharacteristicsSettings;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace User
{
    public class CharacteristicsService : ICharacteristicsService
    {
        private const int INITIAL_CHARACTERISTIC_LEVEL = 1;

        private readonly IMoneyService _moneyService;
        private readonly ICharacteristicsSettingsProvider _characteristicsSettings;
        private readonly Dictionary<CharacteristicType, ReactiveProperty<int>> _characteristicsLevels;

        public CharacteristicsService(ICharacteristicsSettingsProvider characteristicsSettings, IMoneyService moneyService)
        {
            _characteristicsSettings = characteristicsSettings;
            _moneyService = moneyService;
            _characteristicsLevels = new();

            var allCharacteristicTypes = Enum.GetValues(typeof(CharacteristicType))
                .Cast<CharacteristicType>();
            foreach (var characteristicSetting in allCharacteristicTypes)
            {
                //уровни характеристик тут будем заполнять из сохраненных настроек
                _characteristicsLevels[characteristicSetting] = new ReactiveProperty<int>(INITIAL_CHARACTERISTIC_LEVEL);
            }
        }

        public IReadOnlyReactiveProperty<int> GetCharacteristicLevel(CharacteristicType type)
        {
            return _characteristicsLevels[type];
        }

        public void UpgradeCharacteristic(CharacteristicType type)
        {
            var characteristicLevel = _characteristicsLevels[type];
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