using UnityEngine;

[CreateAssetMenu(fileName = "ConfigurationsProvider", menuName = "ConfigurationsProvider")]
public class ConfigurationsProvider : ScriptableObject, IConfigurationProvider
{
    public CharacteristicsSettingsProvider CharacteristicsSettingsProvider => _characteristicsSettingsProvider;
    
    [SerializeField] 
    private CharacteristicsSettingsProvider _characteristicsSettingsProvider;
  

    public void Initialize()
    {
        _characteristicsSettingsProvider.Initialize();
    }
}