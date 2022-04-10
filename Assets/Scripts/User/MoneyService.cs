using UniRx;

namespace User
{
    public class MoneyService : IMoneyService
    {
        public IReadOnlyReactiveProperty<int> Money => _moneyProvider.Money;
        private readonly IMoneyProvider _moneyProvider;


        public MoneyService(IMoneyProvider moneyProvider)
        {
            _moneyProvider = moneyProvider;
        }

        public void Pay(int amount)
        {
            if (HasEnoughMoney(amount))
            {
                _moneyProvider.Money.Value -= amount;
            }
        }
    
        public void Receive(int money)
        {
            _moneyProvider.Money.Value += money;
        }

        public bool HasEnoughMoney(int amount)
        {
            return _moneyProvider.Money.Value >= amount;
        }
    }
}