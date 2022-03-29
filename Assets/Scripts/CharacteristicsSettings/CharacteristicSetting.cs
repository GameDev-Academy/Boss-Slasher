using System;
using UnityEngine;

[Serializable]
public class CharacteristicSetting
{
    public int Level => _level;
    public int Value => _value;
    public int UpgradeCost => _upgradeCost;

    [SerializeField]
    private int _level;
    
    [SerializeField]
    private int _value;
    
    [SerializeField]
    private int _upgradeCost;
}