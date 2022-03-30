using ConfigurationProviders;

namespace User
{
    public class UserProfileService : IUserProfileService
    {
        private UserProfile _playerProfile;

        public UserProfile CreateDefaultUserProfile(IConfigurationProvider configurationProvider)
        {
            _playerProfile = new UserProfile(configurationProvider);
            return _playerProfile;
        }
    }
}