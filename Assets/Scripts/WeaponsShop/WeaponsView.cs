using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using User;
using WeaponsSettings;

namespace WeaponsShop
{
    public class WeaponsView : MonoBehaviour
    {
        [SerializeField] 
        private ScrollButtonsPresenter _scrollButtonsPresenter;

        [SerializeField] 
        private Transform _root;

        private IWeaponsSettingsProvider _weaponsSettingsProvider;

        public void Initialize(IWeaponsSettingsProvider weaponsSettingsProvider, IWeaponsService weaponsService, IMoneyService moneyService)
        {
            _weaponsSettingsProvider = weaponsSettingsProvider;
            
            var prefabs = InstantiateWeapons();
            _scrollButtonsPresenter.Initialize(_weaponsSettingsProvider, weaponsService, moneyService, prefabs);
        }

        private List<GameObject> InstantiateWeapons()
        {
            var weaponIds = _weaponsSettingsProvider.GetWeaponsId().ToList();
            var prefabs = new List<GameObject>();

            for (var i = 0; i < weaponIds.Count; i++)
            {
                var weapon = Instantiate(_weaponsSettingsProvider.GetPrefab(weaponIds[i]), _root);
                weapon.SetActive(false);
                prefabs.Add(weapon);
            }

            prefabs[0].SetActive(true);

            return prefabs;
        }
    }
}