public class MoneyController
{
    private int _money = 9999;

    public void Pay(int amount)
    {
        if (IsEnoughMoney(amount))
        {
            _money -= amount;
        }
    }

    private bool IsEnoughMoney(int amount)
    {
        return _money >= amount;
    }
}