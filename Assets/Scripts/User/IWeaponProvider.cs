using UniRx;

namespace User
{
    public interface IWeaponProvider
    {
        ReactiveCollection<string> Weapons { get; }
        ReactiveProperty<string> CurrentWeapon { get; }
    }
}