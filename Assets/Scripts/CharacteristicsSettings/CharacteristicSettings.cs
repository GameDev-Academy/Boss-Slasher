using System;
using System.Linq;
using UnityEngine;

namespace CharacteristicsSettings
{
    [Serializable]
    public class CharacteristicSettings
    {
        public CharacteristicType Type => _type;

        [SerializeField] 
        private CharacteristicType _type;
    
        [SerializeField] 
        private CharacteristicSetting[] _characteristicSettings;

        public int GetValueByLevel(int level)
        {
            var characteristic = _characteristicSettings.First(characteristicSetting => characteristicSetting.Level == level);
            return characteristic.Value;
        }
    
        public int GetUpgradeCostByLevel(int level)
        {
            var characteristic = _characteristicSettings.First(characteristicSetting => characteristicSetting.Level == level);
            return characteristic.UpgradeCost;
        }
    }
}