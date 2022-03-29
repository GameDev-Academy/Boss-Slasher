using System;
using UnityEngine;

[Serializable]
public class ConfigurationProvider : IConfigurationProvider
{
    public CharacteristicsSettingsProvider CharacteristicsSettingsProvider => _characteristicsSettingsProvider;
    
    [SerializeField] 
    private CharacteristicsSettingsProvider _characteristicsSettingsProvider;
  

    public void Initialize()
    {
        _characteristicsSettingsProvider.Initialize();
    }
}