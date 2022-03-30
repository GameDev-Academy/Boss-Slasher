using CharacteristicsSettings;

namespace ConfigurationsProviders
{
    public interface IConfigurationProvider
    {
        public CharacteristicsSettingsProvider CharacteristicsSettingsProvider { get; }
        void Initialize();
    }
}