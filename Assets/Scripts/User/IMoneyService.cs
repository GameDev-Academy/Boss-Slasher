using UniRx;

namespace User
{
    public interface IMoneyService : IService
    {
        IReadOnlyReactiveProperty<int> Money { get; }
        void Pay(int amount);
        void Receive(int money);
        bool HasEnoughMoney(int amount);
    }
}