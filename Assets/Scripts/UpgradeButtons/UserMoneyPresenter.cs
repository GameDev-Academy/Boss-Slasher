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

        public void Initialize(IMoneyService moneyService)
        {
            //подписка на изменение денег пользователя
            moneyService.Money
                .Subscribe(_ => _userMoneyValue.text = moneyService.Money.Value.ToString())
                .AddTo(this);
        }
    }
}