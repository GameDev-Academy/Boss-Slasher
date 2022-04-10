using UniRx;

public interface IMoneyProvider
{
    ReactiveProperty<int> Money { get; }
}