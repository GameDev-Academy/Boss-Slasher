using System;
using System.Linq;
using UniRx;
using User;

namespace UserProgress
{
    public class ProgressManager : IDisposable
    {
        private CompositeDisposable _subscriptions;

        public ProgressManager(ICharacteristicsService characteristicsService, IMoneyService moneyService)
        {
            _subscriptions = new CompositeDisposable();
        
            var allCharacteristicTypes = Enum.GetValues(typeof(CharacteristicType))
                .Cast<CharacteristicType>();
        
            foreach (var characteristicType in allCharacteristicTypes)
            {
                var level = characteristicsService.GetCharacteristicLevel(characteristicType);
                var upgradeLevelSubscription = level
                    .Subscribe(currentLevel => PrefsManager.SaveLevelProgress(characteristicType, currentLevel));
            
                _subscriptions.Add(upgradeLevelSubscription);
            }

            var moneySubscription = moneyService.Money
                .Subscribe(PrefsManager.SaveMoneyProgress);
            
            _subscriptions.Add(moneySubscription);
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
    }
}