namespace CharacteristicsSettings
{
    public interface ICharacteristicsSettingsProvider
    {
        int GetValue(CharacteristicType type, int level);
        int GetUpgradeCost(CharacteristicType type, int level);
        bool IsLastLevel(CharacteristicType type, int level);
    }
}