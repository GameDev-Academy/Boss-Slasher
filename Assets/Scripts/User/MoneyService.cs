using ConfigurationProviders;
using UniRx;

public class MoneyService : IMoneyService
{
    public IReadOnlyReactiveProperty<int> Money => _money;
    
    private ReactiveProperty<int> _money;
    private IConfigurationProvider _configurationProvider;

    public MoneyService(IConfigurationProvider configurationProvider, int money)
    {
        _configurationProvider = configurationProvider;
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