using UniRx;

namespace User
{
    public interface IWeaponsService : IService
    {
        IReadOnlyReactiveProperty<string> CurrentWeapon { get; }
        IReadOnlyReactiveCollection<string> Weapons { get; }
        
        void BuyWeapon(string id);
        void SelectAsMainWeapon(string id);
        int GetCurrentSelectedWeaponIndex();
        bool HasWeapon(string id);
    }
}