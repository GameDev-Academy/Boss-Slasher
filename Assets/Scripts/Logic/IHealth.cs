using UniRx;

public interface IHealth
{
    IReadOnlyReactiveProperty<bool> IsDead { get; }
    int MaxHealth { get; set; }

    int CurrentHealth { get; set; }
    void TakeDamage(int damage);
}