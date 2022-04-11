using UniRx;
using WeaponsSettings;

namespace User
{
    public interface IWeaponProvider
    {
        ReactiveCollection<string> Weapons { get; }
        public ReactiveProperty<string> CurrentWeapon { get; }
    }
}