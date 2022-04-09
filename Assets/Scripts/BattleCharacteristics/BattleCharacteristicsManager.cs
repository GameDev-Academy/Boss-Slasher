using System;
using System.Collections.Generic;
using ConfigurationProviders;
using UniRx;
using User;

namespace BattleCharacteristics
{
    /// <summary>
    /// BattleCharacteristicsManager - класс, который хранит все характеристики игрока и дает доступ у ним.
    /// </summary>
    public class BattleCharacteristicsManager
    {
        private List<BattleCharacteristic> _battleCharacteristics;
        private Dictionary<CharacteristicType, ReactiveProperty<int>> _battleCharacteristicsByType;

        public BattleCharacteristicsManager(IConfigurationProvider configurationProvider, ICharacteristicsService characteristicsService)
        {
            _battleCharacteristicsByType = new ();
            
            var characteristicTypes = Enum.GetValues(typeof(CharacteristicType));
            foreach (CharacteristicType characteristicType in characteristicTypes)
            {
                var characteristicLevel = characteristicsService.GetCharacteristicLevel(characteristicType).Value;
                var characteristicSettings = configurationProvider.CharacteristicsSettings;
                var characteristicValue = characteristicSettings.GetValue(characteristicType, characteristicLevel);
                
                AddCharacteristic(characteristicType, characteristicValue);
            }
        }

        public ReactiveProperty<int> GetChatacteristicValue(CharacteristicType type)
        {
            return _battleCharacteristicsByType[type];
        }

        private void AddCharacteristic(CharacteristicType characteristicType, int characteristicValue)
        {
            ReactiveProperty<int> reactiveCharacteristicValue = new ReactiveProperty<int>(characteristicValue);
            
            _battleCharacteristicsByType[characteristicType] = reactiveCharacteristicValue;
            _battleCharacteristics.Add(new BattleCharacteristic(characteristicType, reactiveCharacteristicValue));
        }
    }
}