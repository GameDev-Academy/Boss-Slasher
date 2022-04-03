using CharacteristicsSettings;
using UnityEngine;
using UnityEngine.Serialization;

namespace ConfigurationProviders
{
    [CreateAssetMenu(fileName = "ConfigurationProvider", menuName = "ConfigurationProvider")]
    public class ConfigurationProvider : ScriptableObject, IConfigurationProvider
    {
        public ICharacteristicsSettingsProvider CharacteristicsSettings => _characteristicsSettings;
    
        [FormerlySerializedAs("_characteristicsSettingsProvider")]
        [SerializeField] 
        private CharacteristicsSettingsProvider _characteristicsSettings;
  

        public void Initialize()
        {
            _characteristicsSettings.Initialize();
        }
    }
}