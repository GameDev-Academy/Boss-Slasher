using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationProviders;
using UniRx;
using User;
using WeaponsSettings;

namespace UserProgress
{
    /// <summary>
    /// Следит за прогрессом пользователя и сохраняет его через методы PrefsManager
    /// </summary>
    public class ProfileProgressController : IDisposable
    {
        private readonly IWeaponsSettingsProvider _weaponsSettingsProvider;
        private CompositeDisposable _subscriptions;

        public ProfileProgressController(IWeaponsSettingsProvider weaponsSettingsProvider)
        {
            _weaponsSettingsProvider = weaponsSettingsProvider;
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
                .Subscribe(weaponElement => PrefsManager.SaveNewBoughtWeapon(weaponElement.Value));

            _subscriptions.Add(buyWeaponSubscription);
            
            MarkAsBoughtDefaultWeapons(userProfile);
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
            return _weaponsSettingsProvider.GetWeaponsId()
                .Where(PrefsManager.HasWeaponBought)
                .ToList();
        }

        private void MarkAsBoughtDefaultWeapons(UserProfile userProfile)
        {
            foreach (var weapon in userProfile.Weapons)
            {
                if (!PrefsManager.HasWeaponBought(weapon))
                {
                    PrefsManager.SaveNewBoughtWeapon(weapon);
                }
            }
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
    }
}