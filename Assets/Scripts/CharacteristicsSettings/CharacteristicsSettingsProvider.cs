using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace CharacteristicsSettings
{
    [CreateAssetMenu(fileName = "CharacteristicsSettingsProvider", menuName = "CharacteristicsSettingsProvider")]
    public class CharacteristicsSettingsProvider : ScriptableObject
    {
        [SerializeField] 
        private CharacteristicSettings[] _characteristicsSettings;
    
        private Dictionary<CharacteristicType, CharacteristicSettings> _characteristicSettingsByType;

    
        public void Initialize()
        {
            _characteristicSettingsByType = new ();
        
            foreach (var characteristicSettings in _characteristicsSettings)
            {
                _characteristicSettingsByType[characteristicSettings.Type] = characteristicSettings;
            }
        }

        public CharacteristicSettings[] GetAllCharacteristicsSettings()
        {
            return _characteristicsSettings;
        }
        
        public int GetNumberOfLevels(CharacteristicType type)
        {
            var characteristic = GetCharacteristicSettingsByType(type);
            Assert.IsNotNull(characteristic, $"Characteristic is null, please check the type {type} in " +
                                             "characteristicsSettingsProvider");
            
            return characteristic.GetNumberOfLevels();
        }

        public int GetValueByLevel(CharacteristicType type, int level)
        {
            var characteristic = GetCharacteristicSettingsByType(type);
            Assert.IsNotNull(characteristic, $"Characteristic is null, please check the type {type} in " +
                                             "characteristicsSettingsProvider");
            
            return characteristic.GetValueByLevel(level);
        }

        public int GetUpgradeCostByLevel(CharacteristicType type, int level)
        {
            var characteristic = GetCharacteristicSettingsByType(type);
            Assert.IsNotNull(characteristic, $"Characteristic is null, please check the type {type} in " +
                                             "characteristicsSettingsProvider");
            
            return characteristic.GetUpgradeCostByLevel(level);
        }
        
        public bool IsLastCharacteristicLevel(CharacteristicType type, int level)
        {
            var characteristicSettings = GetCharacteristicSettingsByType(type);
            return level >= characteristicSettings.GetNumberOfLevels();
        }

        private CharacteristicSettings GetCharacteristicSettingsByType(CharacteristicType type)
        {
            return _characteristicSettingsByType[type];
        }
    }
}