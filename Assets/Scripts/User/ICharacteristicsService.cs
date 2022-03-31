﻿using UniRx;

public interface ICharacteristicsService
{
    IReadOnlyReactiveProperty<int> GetCharacteristicLevel(CharacteristicType type);
    void UpgradeCharacteristic(CharacteristicType type);
    bool CanUpgradeCharacteristic(CharacteristicType characteristicType, int characteristicLevel);
}