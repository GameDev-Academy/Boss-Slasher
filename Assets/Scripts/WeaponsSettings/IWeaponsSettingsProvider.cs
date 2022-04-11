namespace WeaponsSettings
{
    public interface IWeaponsSettingsProvider
    {
        WeaponSettings GetSettings(string id);
        int GetCost(string id);
    }
}