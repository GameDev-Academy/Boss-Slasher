using ConfigurationProviders;

namespace User
{
    public class UserProfileService : IUserProfileService
    {
        private UserProfile _userProfile;

        public UserProfile CreateDefaultUserProfile(IConfigurationProvider configurationProvider)
        {
            _userProfile = new UserProfile(configurationProvider);
            return _userProfile;
        }
    }
}