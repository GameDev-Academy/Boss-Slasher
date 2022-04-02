using TMPro;
using UniRx;
using UnityEngine;
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

        public void Initialize(IMoneyService moneyService)
        {
            var userMoney = moneyService.Money;

            //подписка на изменение денег пользователя
            userMoney.Subscribe(_ => _userMoneyValue.text = userMoney.Value.ToString()).AddTo(this);
        }
    }
}