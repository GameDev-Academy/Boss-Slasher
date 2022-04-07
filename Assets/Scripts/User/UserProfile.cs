using System.Collections.Generic;
using UniRx;

namespace User
{
    public class UserProfile : ICharacteristicsProvider, IMoneyProvider
    {
        public Dictionary<CharacteristicType, ReactiveProperty<int>> CharacteristicsLevels { get; }
        public ReactiveProperty<int> Money { get; set; }

        public UserProfile()
        {
            CharacteristicsLevels = new Dictionary<CharacteristicType, ReactiveProperty<int>>();
        }
    }
}