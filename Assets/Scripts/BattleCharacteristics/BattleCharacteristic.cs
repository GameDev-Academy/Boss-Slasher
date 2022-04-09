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

        public BattleCharacteristic(CharacteristicType type, ReactiveProperty<int> value)
        {
            _type = type;
            _value = value;
        }
    }
}