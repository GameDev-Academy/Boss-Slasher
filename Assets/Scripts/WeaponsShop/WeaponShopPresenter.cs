using System.Collections.Generic;
using System.Linq;
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
        private Button _previous;

        [SerializeField] 
        private Button _next;

        [SerializeField] 
        private Button _open;

        [SerializeField] 
        private Button _continue;

        [SerializeField] 
        private TextMeshProUGUI _weaponCost;

        private List<GameObject> _weapons;
        private Dictionary<GameObject, string> _weaponsIdsByObject;

        private ReactiveProperty<int> _currentWeaponIndex;

        private IWeaponsSettingsProvider _weaponsSettingsProvider;


        public void Initialize(IWeaponsSettingsProvider weaponsSettingsProvider, 
            IWeaponsService weaponsService,
            IMoneyService moneyService)
        {
            _weaponsSettingsProvider = weaponsSettingsProvider;
            _weapons = InstantiateWeapons();

            _currentWeaponIndex = new ReactiveProperty<int>(0);
            _currentWeaponIndex.Subscribe(ChangeWeaponsCost).AddTo(this);

            _previous.OnClickAsObservable()
                .Subscribe(_ => ShowPrevious())
                .AddTo(this);
            
            _next.OnClickAsObservable()
                .Subscribe(_ => ShowNext())
                .AddTo(this);

            //поток изменений текущего оружия
            var currentWeaponChanges = _currentWeaponIndex
                .Select(_ => !weaponsService.HasWeapon(GetCurrentWeaponId()));

            //поток изменений коллекции оружия
            var weaponsCollectionChanges = weaponsService.Weapons
                .ObserveAdd()
                .Select(element => !weaponsService.HasWeapon(element.Value));

            var openButtonAnimator = _open.gameObject.GetComponent<Animator>();
            var continueButtonAnimator = _continue.gameObject.GetComponent<Animator>();

            var weaponsChanges = currentWeaponChanges.Merge(weaponsCollectionChanges);
            
            weaponsChanges
                .Subscribe(value => openButtonAnimator.SetBool(CAN_BUY, value))
                .AddTo(this);
            
            weaponsChanges
                .Subscribe(value => continueButtonAnimator.SetBool(CAN_BUY, value))
                .AddTo(this);

            currentWeaponChanges.Subscribe(_ => moneyService.Money
                .Select(money => money >= _weaponsSettingsProvider.GetCost(GetCurrentWeaponId()))
                .SubscribeToInteractable(_open))
                .AddTo(this);

            _open.OnClickAsObservable()
                .Subscribe(_ => weaponsService.BuyWeapon(GetCurrentWeaponId()))
                .AddTo(this);

            _continue.OnClickAsObservable()
                .Subscribe(_ => weaponsService.SelectAsMainWeapon(GetCurrentWeaponId()))
                .AddTo(this);
        }

        private List<GameObject> InstantiateWeapons()
        {
            _weaponsIdsByObject = new Dictionary<GameObject, string>();

            var weaponIds = _weaponsSettingsProvider.GetWeaponsId().ToList();
            var weapons = new List<GameObject>();

            for (var i = 0; i < weaponIds.Count; i++)
            {
                var weapon = Instantiate(_weaponsSettingsProvider.GetPrefab(weaponIds[i]), _root);
                weapon.SetActive(false);
                weapons.Add(weapon);

                _weaponsIdsByObject[weapons[i]] = weaponIds[i];
            }

            weapons[0].SetActive(true);

            return weapons;
        }

        private void ShowPrevious()
        {
            _weapons[_currentWeaponIndex.Value].SetActive(false);

            if (_currentWeaponIndex.Value == 0)
            {
                _currentWeaponIndex.Value = _weapons.Count - 1;
            }
            else
            {
                _currentWeaponIndex.Value--;
            }

            _weapons[_currentWeaponIndex.Value].SetActive(true);
        }

        private void ShowNext()
        {
            _weapons[_currentWeaponIndex.Value].SetActive(false);

            if (_currentWeaponIndex.Value == _weapons.Count - 1)
            {
                _currentWeaponIndex.Value = 0;
            }
            else
            {
                _currentWeaponIndex.Value++;
            }

            _weapons[_currentWeaponIndex.Value].SetActive(true);
        }

        private void ChangeWeaponsCost(int index)
        {
            var currentWeapon = _weapons[index];
            var weaponsId = _weaponsIdsByObject[currentWeapon];
            var weaponsCost = _weaponsSettingsProvider.GetCost(weaponsId);

            _weaponCost.text = weaponsCost.ToString();
        }

        private string GetCurrentWeaponId()
        {
            var currentWeapon = _weapons[_currentWeaponIndex.Value];
            return _weaponsIdsByObject[currentWeapon];
        }
    }
}