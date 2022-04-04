using System.Collections.Generic;

public class UserProfile
{
    public Dictionary<CharacteristicType, int> CharacteristicsLevels { get; }
    public int Money { get; set; }

    public UserProfile()
    {
        CharacteristicsLevels = new Dictionary<CharacteristicType, int>();
    }
}