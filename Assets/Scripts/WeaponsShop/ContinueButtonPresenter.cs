using UniRx;
using UnityEngine;
using UnityEngine.UI;
using User;

namespace WeaponsShop
{
    /// <summary>
    /// При клике по кнопке Continue меняет выбранное оружие пользователя
    /// </summary>
    public class ContinueButtonPresenter : MonoBehaviour
    {
        [SerializeField] 
        private Button _continue;

        private ScrollButtonsPresenter _scrollButtonsPresenter;

        public void Initialize(ScrollButtonsPresenter scrollButtonsPresenter, IWeaponsService weaponsService)
        {
            _scrollButtonsPresenter = scrollButtonsPresenter;
        
            //поток изменений текущего оружия
            var weaponsChanges = _scrollButtonsPresenter.CurrentWeaponIndex
                .Select(_ => !weaponsService.HasWeapon(_scrollButtonsPresenter.GetCurrentWeaponId()));

            //поток изменений коллекции оружия
            var weaponsCollectionChanges = weaponsService.Weapons
                .ObserveAdd()
                .Select(element => !weaponsService.HasWeapon(element.Value));

            //подписка на оба потока активностью кнопки Continue
            weaponsChanges.Merge(weaponsCollectionChanges)
                .Subscribe(value => _continue.gameObject.SetActive(!value))
                .AddTo(this);

            _continue.OnClickAsObservable()
                .Subscribe(_ => weaponsService.SelectWeapon(_scrollButtonsPresenter.GetCurrentWeaponId()))
                .AddTo(this);
        }
    }
}