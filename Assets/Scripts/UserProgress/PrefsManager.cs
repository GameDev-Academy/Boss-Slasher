using System;
using System.Linq;
using SimpleEventBus.Disposables;
using UnityEngine;

namespace UserProgress
{
    public class PrefsManager
    {
        private const string MONEY_KEY = "Money";

        public static bool HasUserProfile()
        {
            return PlayerPrefs.HasKey(MONEY_KEY);
        }

        public static void LoadUserProfile(UserProfile userProfile)
        {
            userProfile.Money = PlayerPrefs.GetInt(MONEY_KEY);
        
            var allCharacteristicTypes = Enum.GetValues(typeof(CharacteristicType))
                .Cast<CharacteristicType>();
            foreach (var characteristicType in allCharacteristicTypes)
            {
                var characteristicLevel = PlayerPrefs.GetInt(characteristicType.ToString());
                userProfile.CharacteristicsLevels[characteristicType] = characteristicLevel;
            }
        }

        public static void SaveLevelProgress(CharacteristicType characteristic, int level)
        {
            PlayerPrefs.SetInt(characteristic.ToString(), level);
            PlayerPrefs.Save();
        }
    
        public static void SaveMoneyProgress(int level)
        {
            PlayerPrefs.SetInt(MONEY_KEY, level);
            PlayerPrefs.Save();
        }
    }
}