using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using User;

namespace UserProgress
{
    public class ProfileProgressService : IDisposable
    {
        private CompositeDisposable _subscriptions;

        public void StartTrackingChanges(UserProfile userProfile)
        {
            _subscriptions?.Dispose();

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
            
            var currentWeaponSubscription = userProfile.CurrentWeapon
                .Subscribe(PrefsManager.SaveCurrentWeapon);
            
            _subscriptions.Add(currentWeaponSubscription);
            
            var buyWeaponSubscription = userProfile.Weapons
                .ObserveAdd()
                .Subscribe(weapons => PrefsManager.SaveWeaponProgress(weapons.Value));

            _subscriptions.Add(buyWeaponSubscription);
        }

        public bool HasProgress()
        {
            return PrefsManager.HasUserProfile();
        }

        public UserProfile GetLastUserProfile()
        {
            var characteristics = LoadCharacteristics();
            var userMoney = PrefsManager.LoadMoney();

            var userProfile = new UserProfile(characteristics, userMoney);

            return userProfile;
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }

        private static Dictionary<CharacteristicType, int> LoadCharacteristics()
        {
            var characteristics = new Dictionary<CharacteristicType, int>();

            var allCharacteristicTypes = CharacteristicsTypes.GetAll();
            foreach (var characteristicType in allCharacteristicTypes)
            {
                var characteristicLevel = PrefsManager.LoadCharacteristicLevel(characteristicType);
                characteristics[characteristicType] = characteristicLevel;
            }

            return characteristics;
        }
    }
}