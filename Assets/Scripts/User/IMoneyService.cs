using UniRx;

public interface IMoneyService
{
    IReadOnlyReactiveProperty<int> Money { get; }
    void Pay(int amount);
    void Receive(int money);
    bool IsUserHasEnoughMoneyToUpgrade(CharacteristicType characteristicType, int characteristicLevel);
}