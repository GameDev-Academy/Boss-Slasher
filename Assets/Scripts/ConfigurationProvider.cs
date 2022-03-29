using UnityEngine;

[CreateAssetMenu(fileName = "ConfigurationProvider", menuName = "ConfigurationProvider")]
public class ConfigurationProvider : ScriptableObject, IConfigurationProvider
{
    public CharacteristicsSettingsProvider CharacteristicsSettingsProvider => _characteristicsSettingsProvider;
    
    [SerializeField] 
    private CharacteristicsSettingsProvider _characteristicsSettingsProvider;
  

    public void Initialize()
    {
        _characteristicsSettingsProvider.Initialize();
    }
}