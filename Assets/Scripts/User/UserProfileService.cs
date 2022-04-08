using UserProgress;

namespace User
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ProfileProgressService _profileProgressService;

        public UserProfileService()
        {
            _profileProgressService = new ProfileProgressService();
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
                userProfile = CreateDefaultUserProfile();
            }
            
            _profileProgressService.StartTrackingChanges(userProfile);
            return userProfile;
        }

        private UserProfile CreateDefaultUserProfile()
        {
            return new();
        }

        public void Dispose()
        {
            _profileProgressService.Dispose();
        }
    }
}