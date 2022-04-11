using System.Collections.Generic;
using UniRx;

namespace User
{
    public class UserProfile : ICharacteristicsProvider, IMoneyProvider, IWeaponProvider
    {
        private const int INITIAL_CHARACTERISTIC_LEVEL = 1;
        private const int INITIAL_MONEY_VALUE = 9999;
        private const string INITIAL_WEAPON_ID = "Sword_1";
        
        public Dictionary<CharacteristicType, ReactiveProperty<int>> CharacteristicsLevels { get; }
        public ReactiveProperty<int> Money { get; }
        
        public ReactiveCollection<string> Weapons { get; }

        public ReactiveProperty<string> CurrentWeapon { get; }

        public UserProfile()
        {
            CharacteristicsLevels = new Dictionary<CharacteristicType, ReactiveProperty<int>>();

            var allCharacteristicTypes = CharacteristicsTypes.GetAll();
            foreach (var characteristicType in allCharacteristicTypes)
            {
                CharacteristicsLevels[characteristicType] = new ReactiveProperty<int>(INITIAL_CHARACTERISTIC_LEVEL);
            }

            Money = new ReactiveProperty<int>(INITIAL_MONEY_VALUE);

            Weapons = new ReactiveCollection<string> {INITIAL_WEAPON_ID};

            CurrentWeapon = new ReactiveProperty<string>(INITIAL_WEAPON_ID);
        }

        public UserProfile(Dictionary<CharacteristicType, int> characteristicsLevels, int money, string currentWeapon,
            List<string> weapons)
        {
            CharacteristicsLevels = new Dictionary<CharacteristicType, ReactiveProperty<int>>();
            
            foreach (var characteristic in characteristicsLevels)
            {
                CharacteristicsLevels[characteristic.Key] = new ReactiveProperty<int>(characteristic.Value);
            }
            
            Money = new ReactiveProperty<int>(money);
            
            CurrentWeapon = new ReactiveProperty<string>(currentWeapon);

            Weapons = new ReactiveCollection<string>(weapons);
        }
    }
}