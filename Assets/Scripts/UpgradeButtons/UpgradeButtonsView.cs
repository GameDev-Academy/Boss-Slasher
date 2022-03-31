using ConfigurationProviders;
using UnityEngine;
using User;

public class UpgradeButtonsView : MonoBehaviour
{
    [SerializeField]
    private UpgradeButtonPresenter _buttonPresenter;

    [SerializeField]
    private RectTransform _buttonsRoot;

    [SerializeField] 
    private ConfigurationProvider _configurationProvider;

    private ICharacteristicsService _characteristicsService;
    private IMoneyService _moneyService;

    public void Initialize(ICharacteristicsService characteristicsService, IMoneyService moneyService)
    {
        _characteristicsService = characteristicsService;
        _moneyService = moneyService;

        var characteristics = _configurationProvider.CharacteristicsSettingsProvider.GetAllCharacteristicsSettings();
        foreach (var characteristic in characteristics)
        {
            var characteristicUpgradeButton = Instantiate(_buttonPresenter, _buttonsRoot);
            characteristicUpgradeButton.Initialize(_configurationProvider, characteristic.Type, _characteristicsService, _moneyService);
        }
    }
}