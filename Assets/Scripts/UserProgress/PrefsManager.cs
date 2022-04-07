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

        public static int Load(string key = MONEY_KEY)
        {
            return PlayerPrefs.GetInt(key);
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