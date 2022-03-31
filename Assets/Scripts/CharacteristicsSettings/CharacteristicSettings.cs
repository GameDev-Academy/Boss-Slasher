using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace CharacteristicsSettings
{
    [Serializable]
    public class CharacteristicSettings
    {
        public Sprite Icon;
        
        public CharacteristicType Type => _type;

        [SerializeField] 
        private CharacteristicType _type;
    
        [SerializeField] 
        private CharacteristicSetting[] _characteristicSettings;

        public int GetValueByLevel(int level)
        {
            var characteristic = _characteristicSettings.FirstOrDefault(characteristicSetting => characteristicSetting.Level == level);
            Assert.IsNotNull(characteristic, $"CharacteristicLevel is null, please check the levels of {_type} in " +
                                             "characteristicsSettingsProvider");
            
            return characteristic.Value;
        }
    
        public int GetUpgradeCostByLevel(int level)
        {
            var characteristic = _characteristicSettings.FirstOrDefault(characteristicSetting => characteristicSetting.Level == level);
            Assert.IsNotNull(characteristic, $"CharacteristicLevel is null, please check the levels of {_type} in " +
                                             "characteristicsSettingsProvider");
            
            return characteristic.UpgradeCost;
        }
        
        public int GetNumberOfLevels()
        {
            return _characteristicSettings.Length;
        }
    }
}