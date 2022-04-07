using System;
using System.Linq;
using UniRx;
using User;

namespace UserProgress
{
    public class ProfileProgressService : IDisposable
    {
        private CompositeDisposable _subscriptions;

        public void Initialize(ICharacteristicsService characteristicsService, IMoneyService moneyService)
        {
            _subscriptions = new CompositeDisposable();
        
            var allCharacteristicTypes = Enum.GetValues(typeof(CharacteristicType))
                .Cast<CharacteristicType>();
        
            foreach (var characteristicType in allCharacteristicTypes)
            {
                var level = characteristicsService.GetCharacteristicLevel(characteristicType);
                var upgradeLevelSubscription = level
                    .Subscribe(currentLevel => PrefsManager.SaveLevelProgress(characteristicType, currentLevel));
            
                _subscriptions.Add(upgradeLevelSubscription);
            }

            var moneySubscription = moneyService.Money
                .Subscribe(PrefsManager.SaveMoneyProgress);
            
            _subscriptions.Add(moneySubscription);
        }

        public bool HasProgress()
        {
            return PrefsManager.HasUserProfile();
        }
        
        public UserProfile GetLastUserProfile()
        {
            var userProfile = new UserProfile();
            
            var allCharacteristicTypes = Enum.GetValues(typeof(CharacteristicType))
                .Cast<CharacteristicType>();

            foreach (var characteristicType in allCharacteristicTypes)
            {
                var characteristicLevel = PrefsManager.Load(characteristicType.ToString());
                userProfile.CharacteristicsLevels[characteristicType] = characteristicLevel;
            }

            userProfile.Money = PrefsManager.Load();

            return userProfile;
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
    }
}