using ConfigurationProviders;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonView : MonoBehaviour
{
    [SerializeField]
    private Image _characteristicIcon;
    
    [SerializeField] 
    private TextMeshProUGUI _upgradeCost;

    public void Initialize(IConfigurationProvider configurationProvider, CharacteristicType characteristicType)
    {
        var characteristicsSettingsProvider = configurationProvider.CharacteristicsSettingsProvider;
        _characteristicIcon.sprite = characteristicsSettingsProvider.GetCharacteristicSettingsByType(characteristicType).Icon;
    }

    public void ChangeUpgradeCost(string upgradeCost)
    {
        _upgradeCost.text = upgradeCost;
    }
}