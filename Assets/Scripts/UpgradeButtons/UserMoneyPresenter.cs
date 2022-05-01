using TMPro;
using UniRx;
using UnityEngine;
using User;

namespace UpgradeButtons
{
    /// <summary>
    /// Класс, отвечающий за отображение на UI денег пользователя
    /// </summary>
    public class UserMoneyPresenter : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _userMoneyValue;
        
        private void Awake()
        {
            var moneyService = ServiceLocator.Instance.GetSingle<IMoneyService>();
            
            moneyService.Money
                .Subscribe(_ => _userMoneyValue.text = moneyService.Money.Value.ToString())
                .AddTo(this);
        }
    }
}