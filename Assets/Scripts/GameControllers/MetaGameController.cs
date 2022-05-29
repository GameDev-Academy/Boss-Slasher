using ConfigurationProviders;
using Events;
using UnityEngine;
using UpgradeButtons;
using User;

/// <summary>
/// Инициализирует UpgradeButtonsView и UserMoneyPresenter сервисами
/// </summary>
public class MetaGameController : MonoBehaviour
{
    [SerializeField] 
    private UpgradeButtonsView _upgradeButtonsView;

    private void Awake()
    {
        var configurationProvider = ServiceLocator.Instance.GetSingle<IConfigurationProvider>();
        var characteristicsService = ServiceLocator.Instance.GetSingle<ICharacteristicsService>();
        var moneyService = ServiceLocator.Instance.GetSingle<IMoneyService>();
        
        _upgradeButtonsView.Initialize(configurationProvider, characteristicsService, moneyService);
    }

    public void OpenShop()
    {
        EventStreams.UserInterface.Publish(new OpenShopEvent());
    }

    public void StartBattle()
    {
        EventStreams.UserInterface.Publish(new StartBattleEvent());
    }
}