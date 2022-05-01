using UniRx;

namespace User
{
    public interface IWeaponsService : IService
    {
        IReadOnlyReactiveCollection<string> Weapons { get; }
        
        void BuyWeapon(string id);
        void SelectAsMainWeapon(string id);
        int GetCurrentSelectedWeaponIndex();
        bool HasWeapon(string id);
    }
}