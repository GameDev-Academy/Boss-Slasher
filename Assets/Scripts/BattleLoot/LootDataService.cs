using UniRx;

namespace BattleLoot
{
    /// <summary>
    /// Класс, отвечающий за хранение денег полученных в подземелье
    /// </summary>
    public class LootDataService : ILootDataService
    {
        public IReadOnlyReactiveProperty<int> Money => _collected;
            
        private ReactiveProperty<int> _collected = new ReactiveProperty<int>(0);
        
        public void Collect(int money)
        {
            _collected.Value += money;
        }

        public void ClearData()
        {
            _collected.Value = 0;
        }
    }
}