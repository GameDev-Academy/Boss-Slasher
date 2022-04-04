using System;
using System.Linq;
using UserProgress;

namespace User
{
    public class UserProfileService : IUserProfileService
    {
        private const int INITIAL_CHARACTERISTIC_LEVEL = 1;
        private const int INITIAL_MONEY_VALUE = 9999;
        private UserProfile _userProfile;
    
        public UserProfile CreateUserProfile()
        {
            _userProfile = new UserProfile();
        
            if (PrefsManager.HasUserProfile())
            {
                PrefsManager.LoadUserProfile(_userProfile);
            }
            else
            {
                _userProfile.Money = INITIAL_MONEY_VALUE;
            
                var allCharacteristicTypes = Enum.GetValues(typeof(CharacteristicType))
                    .Cast<CharacteristicType>();
                foreach (var characteristicType in allCharacteristicTypes)
                {
                    _userProfile.CharacteristicsLevels[characteristicType] = INITIAL_CHARACTERISTIC_LEVEL;
                }
            }

            return _userProfile;
        }
    }
}