using System;
using System.Linq;
using UserProgress;

namespace User
{
    public class UserProfileService : IUserProfileService
    {
        private const int INITIAL_CHARACTERISTIC_LEVEL = 1;
        private const int INITIAL_MONEY_VALUE = 9999;

        private ProfileProgressService _profileProgressService;

        public UserProfileService()
        {
            _profileProgressService = new ProfileProgressService();
        }
        
        public void Initialize(ICharacteristicsService characteristicsService, IMoneyService moneyService)
        {
            _profileProgressService.Initialize(characteristicsService, moneyService);
        }
        
        public UserProfile LoadOrCreateDefaultProfile()
        {
            if (_profileProgressService.HasProgress())
            {
               return _profileProgressService.GetLastUserProfile();
            }

            return CreateDefaultUserProfile();
        }

        private UserProfile CreateDefaultUserProfile()
        {
            var userProfile = new UserProfile();
            
            var allCharacteristicTypes = Enum.GetValues(typeof(CharacteristicType))
                .Cast<CharacteristicType>();
            foreach (var characteristicType in allCharacteristicTypes)
            {
                userProfile.CharacteristicsLevels[characteristicType] = INITIAL_CHARACTERISTIC_LEVEL;
            }

            userProfile.Money = INITIAL_MONEY_VALUE;

            return userProfile;
        }

        public void Dispose()
        {
            _profileProgressService.Dispose();
        }
    }
}