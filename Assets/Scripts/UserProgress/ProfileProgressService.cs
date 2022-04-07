using System;
using System.Linq;
using UniRx;
using User;

namespace UserProgress
{
    public class ProfileProgressService : IDisposable
    {
        private CompositeDisposable _subscriptions;

        public void StartTrackingChanges(UserProfile userProfile)
        {
            _subscriptions = new CompositeDisposable();

            foreach (var characteristic in userProfile.CharacteristicsLevels)
            {
                var upgradeLevelSubscription = characteristic.Value
                    .Subscribe(currentLevel => PrefsManager.SaveLevelProgress(characteristic.Key, currentLevel));

                _subscriptions.Add(upgradeLevelSubscription);
            }

            var moneySubscription = userProfile.Money
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
                userProfile.CharacteristicsLevels[characteristicType] = new ReactiveProperty<int>(characteristicLevel);
            }

            var userMoney = PrefsManager.Load();
            userProfile.Money = new ReactiveProperty<int>(userMoney);

            return userProfile;
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
    }
}