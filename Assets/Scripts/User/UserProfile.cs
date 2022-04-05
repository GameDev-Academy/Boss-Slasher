using System.Collections.Generic;

namespace User
{
    public class UserProfile
    {
        public Dictionary<CharacteristicType, int> CharacteristicsLevels { get; }
        public int Money { get; set; }

        public UserProfile()
        {
            CharacteristicsLevels = new Dictionary<CharacteristicType, int>();
        }
    }
}