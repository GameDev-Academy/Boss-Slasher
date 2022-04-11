using UniRx;

namespace BattleCharacteristics
{
    /// <summary>
    /// BattleCharacteristic - класс для хранения данных одной характеристики 
    /// </summary>
    public class BattleCharacteristic
    {
        public IReadOnlyReactiveProperty<int> Value => _value;
        public readonly CharacteristicType Type;

        private readonly CharacteristicType _type;
        private ReactiveProperty<int> _value;

        public BattleCharacteristic(CharacteristicType type, int value)
        {
            _type = type;
            _value = new ReactiveProperty<int>(value);
        }

        public static BattleCharacteristic operator +(BattleCharacteristic a, int b) 
            => new BattleCharacteristic(a._type, a.Value.Value + b);
        
        public static BattleCharacteristic operator -(BattleCharacteristic a, int b) 
            => new BattleCharacteristic(a._type, a.Value.Value - b);
    }
}