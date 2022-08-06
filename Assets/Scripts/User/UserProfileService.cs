using System;
using UserProgress;
using WeaponsSettings;

namespace User
{
    public class UserProfileFactory : IUserProfileFactory, IDisposable
    {
        private readonly ProfileProgressController _profileProgressController;
        private UserProfile _userProfile;

        public UserProfileFactory(IWeaponsSettingsProvider weaponsSettingsProvider)
        {
            _profileProgressController = new ProfileProgressController(weaponsSettingsProvider);
        }

        public UserProfile CreateOrLoadProfile()
        {
            if (_profileProgressController.HasProgress())
            {
                _userProfile = _profileProgressController.GetLastUserProfile();
            }
            else
            {
                _userProfile = new UserProfile();
            }

            _profileProgressController.StartTrackingChanges(_userProfile);
            return _userProfile;
        }

        public void Dispose()
        {
            _profileProgressController.Dispose();
        }
    }
}