using UniRx;

namespace User
{
    public interface IWeaponsService
    {
        IReadOnlyReactiveCollection<string> Weapons { get; }
        
        void BuyWeapon(string id);
        void SelectWeapon(string id);
        bool HasWeapon(string id);
    }
}