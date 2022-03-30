using CharacteristicsSettings;

namespace ConfigurationProviders
{
    public interface IConfigurationProvider
    {
        public CharacteristicsSettingsProvider CharacteristicsSettingsProvider { get; }
        void Initialize();
    }
}