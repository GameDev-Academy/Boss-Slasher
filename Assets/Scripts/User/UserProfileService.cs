using System;
using ConfigurationProviders;
using UserProgress;
using WeaponsSettings;

namespace User
{
    public class UserProfileService : IUserProfileService, IDisposable
    {
        private readonly ProfileProgressService _profileProgressService;

        public UserProfileService(IWeaponsSettingsProvider weaponsSettingsProvider)
        {
            _profileProgressService = new ProfileProgressService(weaponsSettingsProvider);
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