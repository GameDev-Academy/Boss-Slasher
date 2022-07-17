using System;
using ConfigurationProviders;
using UserProgress;
using WeaponsSettings;

namespace User
{
    public class UserProfileService : IUserProfileService, IDisposable
    {
        private readonly ProfileProgressService _profileProgressService;
        private UserProfile _userProfile;

        public UserProfileService(IWeaponsSettingsProvider weaponsSettingsProvider)
        {
            _profileProgressService = new ProfileProgressService(weaponsSettingsProvider);
        }
        
        UserProfile IUserProfileService.GetCurrentProfile()
        {
            return _userProfile;
        }

        public UserProfile CreateNewOrGetLastProfile()
        {
            if (_profileProgressService.HasProgress())
            {
                _userProfile = _profileProgressService.GetLastUserProfile();
            }
            else
            {
                _userProfile = new UserProfile();
            }

            _profileProgressService.StartTrackingChanges(_userProfile);
            return _userProfile;
        }

        public void Dispose()
        {
            _profileProgressService.Dispose();
        }
    }
}