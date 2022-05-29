using UniRx;

namespace User
{
    public interface ICharacteristicsService : IService
    {
        IReadOnlyReactiveProperty<int> GetCharacteristicLevel(CharacteristicType type);
        void UpgradeCharacteristic(CharacteristicType type);
    }
}