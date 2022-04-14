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
        private Transform _root;

        [SerializeField] 
        private ScrollButtonsPresenter _scrollButtonsPresenter;

        [SerializeField] 
        private OpenButtonPresenter _openButtonPresenter;
    
        [SerializeField] 
        private ContinueButtonPresenter _continueButtonPresenter;

        private IWeaponsSettingsProvider _weaponsSettingsProvider;

        public void Initialize(IWeaponsSettingsProvider weaponsSettingsProvider, IWeaponsService weaponsService, IMoneyService moneyService)
        {
            _weaponsSettingsProvider = weaponsSettingsProvider;
            
            var prefabs = InstantiateWeapons();
            _scrollButtonsPresenter.Initialize(_weaponsSettingsProvider, prefabs);
            _openButtonPresenter.Initialize(_scrollButtonsPresenter, weaponsSettingsProvider, weaponsService, moneyService);
            _continueButtonPresenter.Initialize(_scrollButtonsPresenter, weaponsService);
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