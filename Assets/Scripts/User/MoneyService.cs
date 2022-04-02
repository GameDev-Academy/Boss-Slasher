using ConfigurationProviders;
using UniRx;

namespace User
{
    public class MoneyService : IMoneyService
    {
        public IReadOnlyReactiveProperty<int> Money => _money;
    
        private ReactiveProperty<int> _money;

        public MoneyService(int money)
        {
            _money = new ReactiveProperty<int>(money);
        }

        public void Pay(int amount)
        {
            if (HasEnoughMoney(amount))
            {
                _money.Value -= amount;
            }
        }
    
        public void Receive(int money)
        {
            _money.Value += money;
        }

        public bool HasEnoughMoney(int amount)
        {
            return _money.Value >= amount;
        }
    }
}