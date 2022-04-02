using UniRx;

namespace User
{
    public interface IMoneyService
    {
        IReadOnlyReactiveProperty<int> Money { get; }
        void Pay(int amount);
        void Receive(int money);
    }
}