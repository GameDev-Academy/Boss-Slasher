using UniRx;

namespace BattleLoot
{
    public interface IDungeonMoneyService : IService
    {
        IReadOnlyReactiveProperty<int> Money { get; }
        void Collect(int money);
    }
}