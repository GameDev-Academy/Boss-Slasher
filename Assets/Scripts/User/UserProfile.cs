using System.Collections.Generic;
using UniRx;

namespace User
{
    public class UserProfile : ICharacteristicsProvider, IMoneyProvider
    {
        private const int INITIAL_CHARACTERISTIC_LEVEL = 1;
        private const int INITIAL_MONEY_VALUE = 9999;
        
        public Dictionary<CharacteristicType, ReactiveProperty<int>> CharacteristicsLevels { get; }
        public ReactiveProperty<int> Money { get; }

        public UserProfile()
        {
            CharacteristicsLevels = new Dictionary<CharacteristicType, ReactiveProperty<int>>();

            var allCharacteristicTypes = CharacteristicsTypes.GetAll();
            foreach (var characteristicType in allCharacteristicTypes)
            {
                CharacteristicsLevels[characteristicType] = new ReactiveProperty<int>(INITIAL_CHARACTERISTIC_LEVEL);
            }

            Money = new ReactiveProperty<int>(INITIAL_MONEY_VALUE);
        }

        public UserProfile(Dictionary<CharacteristicType, int> characteristicsLevels, int money)
        {
            CharacteristicsLevels = new Dictionary<CharacteristicType, ReactiveProperty<int>>();
            
            foreach (var characteristic in characteristicsLevels)
            {
                CharacteristicsLevels[characteristic.Key] = new ReactiveProperty<int>(characteristic.Value);
            }
            
            Money = new ReactiveProperty<int>(money);
        }
    }
}