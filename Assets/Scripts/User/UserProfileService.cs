using System;
using System.Linq;
using UniRx;
using UserProgress;

namespace User
{
    public class UserProfileService : IUserProfileService
    {
        private const int INITIAL_CHARACTERISTIC_LEVEL = 1;
        private const int INITIAL_MONEY_VALUE = 9999;

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
            var userProfile = new UserProfile();
            
            var allCharacteristicTypes = Enum.GetValues(typeof(CharacteristicType))
                .Cast<CharacteristicType>();
            foreach (var characteristicType in allCharacteristicTypes)
            {
                userProfile.CharacteristicsLevels[characteristicType] = new ReactiveProperty<int>(INITIAL_CHARACTERISTIC_LEVEL);
            }

            userProfile.Money = new ReactiveProperty<int>(INITIAL_MONEY_VALUE);

            return userProfile;
        }

        public void Dispose()
        {
            _profileProgressService.Dispose();
        }
    }
}