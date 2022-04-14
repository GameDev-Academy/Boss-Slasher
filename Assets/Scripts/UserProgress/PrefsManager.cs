using UnityEngine;

namespace UserProgress
{
    public class PrefsManager
    {
        private const string MONEY_KEY = "Money";
        private const string WEAPON_KEY = "Weapon";
        private const string WEAPON_BOUGHT_KEY = "WeaponToBougth_{0}";

        public static bool HasUserProfile()
        {
            return PlayerPrefs.HasKey(MONEY_KEY);
        }

        public static int LoadCharacteristicLevel(CharacteristicType type)
        {
            return PlayerPrefs.GetInt(type.ToString());
        }

        public static void SaveLevelProgress(CharacteristicType characteristic, int level)
        {
            PlayerPrefs.SetInt(characteristic.ToString(), level);
            PlayerPrefs.Save();
        }

        public static int LoadMoney()
        {
            return PlayerPrefs.GetInt(MONEY_KEY);
        }

        public static void SaveMoneyProgress(int level)
        {
            PlayerPrefs.SetInt(MONEY_KEY, level);
            PlayerPrefs.Save();
        }

        public static bool HasWeaponBought(string id)
        {
            return PlayerPrefs.GetInt(string.Format(WEAPON_BOUGHT_KEY, id)) == 1;
        }

        public static string LoadWeapon()
        {
            return PlayerPrefs.GetString(WEAPON_KEY);
        }

        public static void SaveCurrentWeapon(string id)
        {
            PlayerPrefs.SetString(WEAPON_KEY, id);
            PlayerPrefs.Save();
        }

        public static void SaveNewBoughtWeapon(string id)
        {
            var key = string.Format(WEAPON_BOUGHT_KEY, id);
            PlayerPrefs.SetInt(key, 1);
            PlayerPrefs.Save();
        }
    }
}