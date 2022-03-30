using System.Collections.Generic;
using CharacteristicsSettings;
using UniRx;
using UnityEngine.Assertions;

public class UserProfile
{
    private CharacteristicsSettingsProvider _characteristicsSettingsProvider;

    private Dictionary<CharacteristicType, ReactiveProperty<int>> _characteristicsLevels;

    private Wallet _wallet;
    

    public UserProfile(CharacteristicsSettingsProvider characteristicsSettingsProvider)
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
        var characteristicLevel = _characteristicsLevels[type];
        Assert.IsNotNull(characteristicLevel, $"CharacteristicLevel is null, please check the levels of {type} in " +
                                              "characteristicsSettingsProvider");
        
        _characteristicsLevels[type].Value += upgradeLevelValue;
        _wallet.Pay(upgradeCost);
    }
}