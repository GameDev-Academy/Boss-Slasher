using UniRx;

namespace User
{
    public interface ICharacteristicsService
    {
        IReadOnlyReactiveProperty<int> GetCharacteristicLevel(CharacteristicType type);
        void UpgradeCharacteristic(CharacteristicType type);
    }
}