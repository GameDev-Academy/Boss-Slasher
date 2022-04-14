using UniRx;

namespace BattleCharacteristics
{
    /// <summary>
    /// BattleCharacteristic - класс для хранения данных одной характеристики 
    /// </summary>
    public class BattleCharacteristic
    {
        public ReactiveProperty<int> Value => _value;

        private readonly CharacteristicType _type;
        private ReactiveProperty<int> _value;

        public BattleCharacteristic(CharacteristicType type, int value)
        {
            _type = type;
            _value = new ReactiveProperty<int>(value);
        }

        public static BattleCharacteristic operator +(BattleCharacteristic characteristic, int addend)
        {
            characteristic.Value.Value += addend;
            return characteristic;
        }

        public static BattleCharacteristic operator -(BattleCharacteristic characteristic, int subtrahend)
        {
            characteristic.Value.Value -= subtrahend;
            return characteristic;
        }
    }
}