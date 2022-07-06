using UniRx;

namespace BattleLoot
{
    /// <summary>
    /// The class keeps the money from the dungeon 
    /// </summary>
    public class DungeonMoneyService : IDungeonMoneyService
    {
        public IReadOnlyReactiveProperty<int> Money => _money;

        private ReactiveProperty<int> _money = new ReactiveProperty<int>(0);

        public void Collect(int money)
        {
            _money.Value += money;
        }
    }
}