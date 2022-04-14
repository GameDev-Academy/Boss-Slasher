using UniRx;
using UnityEngine;
using UnityEngine.UI;
using User;
using WeaponsSettings;

namespace WeaponsShop
{
    /// <summary>
    /// При клике по кнопке Open покупает оружие
    /// </summary>
    public class OpenButtonPresenter : MonoBehaviour
    {
        [SerializeField] private Button _open;

        private IWeaponsSettingsProvider _weaponsSettingsProvider;

        private ScrollButtonsPresenter _scrollButtonsPresenter;

        public void Initialize(ScrollButtonsPresenter scrollButtonsPresenter,
            IWeaponsSettingsProvider weaponsSettingsProvider, IWeaponsService weaponsService,
            IMoneyService moneyService)
        {
            _weaponsSettingsProvider = weaponsSettingsProvider;
            _scrollButtonsPresenter = scrollButtonsPresenter;

            //поток изменений текущего оружия
            var weaponsChanges = _scrollButtonsPresenter.CurrentWeaponIndex
                .Select(_ => !weaponsService.HasWeapon(_scrollButtonsPresenter.GetCurrentWeaponId()));

            //поток изменений коллекции оружия
            var weaponsCollectionChanges = weaponsService.Weapons
                .ObserveAdd()
                .Select(element => !weaponsService.HasWeapon(element.Value));

            //подписка на оба потока активностью кнопки Open
            weaponsChanges.Merge(weaponsCollectionChanges)
                .Subscribe(value => _open.gameObject.SetActive(value))
                .AddTo(this);

            weaponsChanges.Subscribe(_ => moneyService.Money
                .Select(HasEnoughMoney)
                .SubscribeToInteractable(_open))
                .AddTo(this);

            _open.OnClickAsObservable()
                .Subscribe(_ => weaponsService.BuyWeapon(_scrollButtonsPresenter.GetCurrentWeaponId()))
                .AddTo(this);
        }

        private bool HasEnoughMoney(int money)
        {
            var weaponCost = _weaponsSettingsProvider.GetCost(_scrollButtonsPresenter.GetCurrentWeaponId());
            return money >= weaponCost;
        }
    }
}