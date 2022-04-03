using CharacteristicsSettings;

namespace ConfigurationProviders
{
    public interface IConfigurationProvider
    {
        public ICharacteristicsSettingsProvider CharacteristicsSettings { get; }
        void Initialize();
    }
}