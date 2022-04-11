using ConfigurationProviders;
using UserProgress;

namespace User
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ProfileProgressService _profileProgressService;

        public UserProfileService(IConfigurationProvider configurationProvider)
        {
            _profileProgressService = new ProfileProgressService(configurationProvider);
        }

        public UserProfile GetProfile()
        {
            UserProfile userProfile;
            
            if (_profileProgressService.HasProgress())
            {
                userProfile = _profileProgressService.GetLastUserProfile();
            }
            else
            {
                userProfile = new UserProfile();
            }
            
            _profileProgressService.StartTrackingChanges(userProfile);
            return userProfile;
        }

        public void Dispose()
        {
            _profileProgressService.Dispose();
        }
    }
}