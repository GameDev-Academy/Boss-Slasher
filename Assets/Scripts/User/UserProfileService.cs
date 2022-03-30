using ConfigurationProviders;

namespace User
{
    public class UserProfileService : IUserProfileService
    {
        private UserProfile _playerProfile;

        public UserProfile CreateDefaultProfile(IConfigurationProvider configurationProvider)
        {
            _playerProfile = new UserProfile(configurationProvider.CharacteristicsSettingsProvider);
            return _playerProfile;
        }
    }
}