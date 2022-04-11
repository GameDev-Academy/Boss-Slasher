using UniRx;
using WeaponsSettings;

namespace User
{
    public interface IWeaponProvider
    {
        ReactiveCollection<WeaponSettings> Weapons { get; }
        public ReactiveProperty<WeaponSettings> CurrentWeapon { get; }
    }
}