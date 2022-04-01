using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using User;

namespace UpgradeButtons
{
    /// <summary>
    /// Класс, отвечающий за отображение на UI денег пользователя
    /// </summary>
    public class UserMoneyPresenter: MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _userMoneyValue;

        private IMoneyService _moneyService;

        public void Initialize(IMoneyService moneyService)
        {
            _moneyService = moneyService;
            var userMoney = _moneyService.Money;

            //подписка на изменение денег пользователя
            userMoney.ObserveEveryValueChanged(money => money.Value)
                .Subscribe(_ => _userMoneyValue.text = userMoney.Value.ToString()).AddTo(this);
        }
    }
}