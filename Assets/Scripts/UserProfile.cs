using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "UserProfile", menuName = "UserProfile")]
public class UserProfile : ScriptableObject
{
    private CharacteristicsSettingsProvider _characteristicsSettingsProvider;

    private Dictionary<CharacteristicType, IReadOnlyReactiveProperty<int>> _characteristicsLevels;

    private Wallet _wallet;
    

    public void Initialize(CharacteristicsSettingsProvider characteristicsSettingsProvider)
    {
        _characteristicsLevels = new ();

        var characteristicsSettings = characteristicsSettingsProvider.GetAllCharacteristicsSettings();
        foreach (var characteristicSetting in characteristicsSettings)
        {
            //уровни характеристик тут будем заполнять из сохраненных настроек
            _characteristicsLevels[characteristicSetting.Type] = new ReactiveProperty<int>(1);
        }

        //деньги будем брать из сохраненных настроек
        _wallet = new Wallet();
    }
    
    public IReadOnlyReactiveProperty<int> GetCharacteristicLevel(CharacteristicType type)
    {
        return _characteristicsLevels[type];
    }
    
    public void UpgradeCharacteristic(CharacteristicType type, int upgradeLevelValue, int upgradeCost)
    {
        ((ReactiveProperty<int>) GetCharacteristicLevel(type)).Value += upgradeLevelValue;
        _wallet.Pay(upgradeCost);
    }
}