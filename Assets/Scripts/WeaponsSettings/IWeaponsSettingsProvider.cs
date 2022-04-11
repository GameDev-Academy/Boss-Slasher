namespace WeaponsSettings
{
    public interface IWeaponsSettingsProvider
    {
        WeaponSettings GetWeapon(string id);
        WeaponSettings[] GetWeapons();
    }
}