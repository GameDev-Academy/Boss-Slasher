using UnityEngine;

namespace UserProgress
{
    public class PrefsManager
    {
        private const string MONEY_KEY = "Money";
        private const string WEAPON_KEY = "Weapon";

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

        public static bool HasWeapon(string id)
        {
            return PlayerPrefs.HasKey(id);
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

        public static void SaveWeaponProgress(string id)
        {
            PlayerPrefs.SetString(id, id);
            PlayerPrefs.Save();
        }
    }
}