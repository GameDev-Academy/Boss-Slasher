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

    [SerializeField] 
    private UserMoneyPresenter _userMoneyPresenter;

    public void Initialize(ICharacteristicsService characteristicsService, IMoneyService moneyService)
    {
        _upgradeButtonsView.Initialize(characteristicsService, moneyService);
        _userMoneyPresenter.Initialize(moneyService);
    }
}