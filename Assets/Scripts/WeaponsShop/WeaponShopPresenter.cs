using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MicroRx.Core;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using User;
using WeaponsSettings;

namespace WeaponsShop
{
    /// <summary>
    /// Презентер магазина оружия
    /// </summary>
    public class WeaponShopPresenter : MonoBehaviour
    {
        private static readonly int CAN_BUY = Animator.StringToHash("canBuy");

        [SerializeField] 
        private Transform _root;

        [SerializeField] 
        private Button _buyButton;

        [SerializeField] 
        private Animator _buyButtonAnimator;

        [SerializeField] 
        private TextMeshProUGUI _weaponCostLabel;

        private List<GameObject> _weapons;
        private Dictionary<GameObject, string> _weaponsIdsByObject;

        private IWeaponsSettingsProvider _weaponsSettingsProvider;
        private IWeaponsService _weaponsService;
        private ReactiveProperty<int> _currentWeaponIndex;


        public void Initialize(IWeaponsSettingsProvider weaponsSettingsProvider, 
            IWeaponsService weaponsService,
            IMoneyService moneyService)
        {
            _weaponsSettingsProvider = weaponsSettingsProvider;
            _weaponsService = weaponsService;
            InstantiateWeapons();

            var currentWeapon = _weaponsService.CurrentWeapon.Value;
            _currentWeaponIndex = new ReactiveProperty<int>(_weaponsSettingsProvider.GetIndex(currentWeapon));
            
            _currentWeaponIndex.Subscribe(UpdateWeaponView).AddTo(this);

            _currentWeaponIndex
                .Select(_ => moneyService.Money.Value >= _weaponsSettingsProvider.GetCost(GetCurrentWeaponId()))
                .SubscribeToInteractable(_buyButton)
                .AddTo(this);
        }

        [UsedImplicitly]
        public void ShowPrevious()
        { 
            _currentWeaponIndex.Value = (_currentWeaponIndex.Value - 1 + _weapons.Count) % _weapons.Count;
        }

        [UsedImplicitly]
        public void ShowNext()
        {
            _currentWeaponIndex.Value = (_currentWeaponIndex.Value + 1) % _weapons.Count;
        }

        [UsedImplicitly]
        public void BuyWeapon()
        {
            _weaponsService.BuyWeapon(GetCurrentWeaponId());
            UpdateBuyButton();
        }

        [UsedImplicitly]
        public void SelectAsMainWeapon()
        {
            _weaponsService.SelectWeapon(GetCurrentWeaponId());
        }

        private void InstantiateWeapons()
        {
            _weaponsIdsByObject = new Dictionary<GameObject, string>();

            var weaponsId = _weaponsSettingsProvider.GetWeaponsId().ToList();
            _weapons = new List<GameObject>();

            for (var i = 0; i < weaponsId.Count; i++)
            {
                var weapon = Instantiate(_weaponsSettingsProvider.GetPrefab(weaponsId[i]), _root);
                weapon.SetActive(false);
                _weapons.Add(weapon);

                _weaponsIdsByObject[_weapons[i]] = weaponsId[i];
            }
        }

        private void UpdateWeaponView(int previousIndex, int currentIndex)
        {
            _weapons[previousIndex].SetActive(false);
            _weapons[currentIndex].SetActive(true);
            
            UpdateWeaponCost(currentIndex);
            UpdateBuyButton();
        }

        private void UpdateWeaponCost(int currentIndex)
        {
            var currentWeapon = _weapons[currentIndex];
            var weaponsId = _weaponsIdsByObject[currentWeapon];
            var weaponsCost = _weaponsSettingsProvider.GetCost(weaponsId);
            _weaponCostLabel.text = weaponsCost.ToString();
        }

        private void UpdateBuyButton()
        {
            var canBuy = !_weaponsService.HasWeapon(GetCurrentWeaponId());
            _buyButtonAnimator.SetBool(CAN_BUY, canBuy);
        }

        private string GetCurrentWeaponId()
        {
            var currentWeapon = _weapons[_currentWeaponIndex.Value];
            return _weaponsIdsByObject[currentWeapon];
        }
    }
}