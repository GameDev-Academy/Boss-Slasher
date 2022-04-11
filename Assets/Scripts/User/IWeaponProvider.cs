using UniRx;

namespace User
{
    public interface IWeaponProvider
    {
        ReactiveCollection<string> Weapons { get; }
        public ReactiveProperty<string> CurrentWeapon { get; }
    }
}