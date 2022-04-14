using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationProviders;
using UniRx;
using User;

namespace UserProgress
{
    public class ProfileProgressService : IDisposable
    {
        private IConfigurationProvider _configurationProvider;
        private CompositeDisposable _subscriptions;

        public ProfileProgressService(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

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
                .Subscribe(weapons => PrefsManager.SaveNewBoughtWeapon(weapons.Value));

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
            var currentWeapon = PrefsManager.LoadWeapon();
            var weapons = LoadWeapons();

            var userProfile = new UserProfile(characteristics, userMoney, currentWeapon, weapons);

            return userProfile;
        }

        private Dictionary<CharacteristicType, int> LoadCharacteristics()
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

        private List<string> LoadWeapons()
        {
            var weaponsSettingsProvider = _configurationProvider.WeaponsSettingsProvider;

            return weaponsSettingsProvider.GetWeaponsId()
                .Where(PrefsManager.HasWeaponBought)
                .ToList();
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
    }
}