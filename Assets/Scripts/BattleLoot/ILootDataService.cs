using UniRx;

namespace BattleLoot
{
    public interface ILootDataService : IService
    {
        IReadOnlyReactiveProperty<int> Money { get; }
        void Collect(int money);
        void ClearData();
    }
}