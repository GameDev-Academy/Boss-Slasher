using System.Collections.Generic;
using UnityEngine;

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

    public int GetValueByLevel(CharacteristicType type, int level)
    {
        return GetCharacteristicSettingsByType(type).GetValueByLevel(level);
    }

    public int GetUpgradeCostByLevel(CharacteristicType type, int level)
    {
        return GetCharacteristicSettingsByType(type).GetUpgradeCostByLevel(level);;
    }

    private CharacteristicSettings GetCharacteristicSettingsByType(CharacteristicType type)
    {
        return _characteristicSettingsByType[type];
    }
}