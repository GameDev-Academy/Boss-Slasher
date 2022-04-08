using System.Collections.Generic;
using UniRx;

public interface ICharacteristicsProvider
{
    Dictionary<CharacteristicType, ReactiveProperty<int>> CharacteristicsLevels { get; }
}