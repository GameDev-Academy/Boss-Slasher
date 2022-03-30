using UniRx;

public class MoneyController
{
    public IReadOnlyReactiveProperty<int> Money => _money;
    
    private ReactiveProperty<int> _money;

    public MoneyController(int money)
    {
        _money = new ReactiveProperty<int>(money);
    }
    
    public void Pay(int amount)
    {
        if (IsEnoughMoney(amount))
        {
            _money.Value -= amount;
        }
    }
    
    public void Receive(int money)
    {
        _money.Value += money;
    }

    private bool IsEnoughMoney(int amount)
    {
        return _money.Value >= amount;
    }
}