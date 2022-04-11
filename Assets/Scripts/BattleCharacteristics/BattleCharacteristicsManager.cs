using System.Collections.Generic;
using ConfigurationProviders;
using User;

namespace BattleCharacteristics
{
    /// <summary>
    /// BattleCharacteristicsManager - класс, который хранит все характеристики игрока и дает доступ к ним.
    /// </summary>
    public class BattleCharacteristicsManager
    {
        private readonly Dictionary<CharacteristicType, BattleCharacteristic> _battleCharacteristics;

        public BattleCharacteristicsManager(IConfigurationProvider configurationProvider,
            ICharacteristicsService characteristicsService)
        {
            _battleCharacteristics = new();

            var characteristicTypes = CharacteristicsTypes.GetAll();
            foreach (CharacteristicType characteristicType in characteristicTypes)
            {
                var characteristicLevel = characteristicsService.GetCharacteristicLevel(characteristicType).Value;
                var characteristicSettings = configurationProvider.CharacteristicsSettings;
                var characteristicValue = characteristicSettings.GetValue(characteristicType, characteristicLevel);

                AddCharacteristic(characteristicType, characteristicValue);
            }
        }

        public BattleCharacteristic GetChatacteristic(CharacteristicType type)
        {
            return _battleCharacteristics[type];
        }

        private void AddCharacteristic(CharacteristicType characteristicType, int characteristicValue)
        {
            _battleCharacteristics[characteristicType] = new BattleCharacteristic(characteristicType, characteristicValue);
        }
    }
}