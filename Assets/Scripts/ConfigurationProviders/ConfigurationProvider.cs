using CharacteristicsSettings;
using UnityEngine;
using UnityEngine.Serialization;
using WeaponsSettings;

namespace ConfigurationProviders
{
    [CreateAssetMenu(fileName = "ConfigurationProvider", menuName = "ConfigurationProvider")]
    public class ConfigurationProvider : ScriptableObject, IConfigurationProvider
    {
        public ICharacteristicsSettingsProvider CharacteristicsSettings => _characteristicsSettings;
        public IWeaponsSettingsProvider WeaponsSettingsProvider => _weaponsSettingsProvider;
    
        [FormerlySerializedAs("_characteristicsSettingsProvider")]
        [SerializeField] 
        private CharacteristicsSettingsProvider _characteristicsSettings;

        [SerializeField] 
        private WeaponsSettingsProvider _weaponsSettingsProvider;
  
        public void Initialize()
        {
            _characteristicsSettings.Initialize();
        }
    }
}