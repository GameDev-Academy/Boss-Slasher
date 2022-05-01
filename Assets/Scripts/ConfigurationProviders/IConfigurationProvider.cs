using CharacteristicsSettings;
using WeaponsSettings;

namespace ConfigurationProviders
{
    public interface IConfigurationProvider : IService
    {
        public ICharacteristicsSettingsProvider CharacteristicsSettings { get; }
        public IWeaponsSettingsProvider WeaponsSettingsProvider { get; }
        
        void Initialize();
    }
}