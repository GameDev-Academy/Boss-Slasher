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

    private bool IsEnoughMoney(int amount)
    {
        return _money.Value >= amount;
    }
}