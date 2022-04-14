using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using User;
using WeaponsSettings;

public class WeaponsView : MonoBehaviour
{
    [SerializeField] 
    private WeaponsSettingsProvider _weaponsSettingsProvider;

    [SerializeField] 
    private ScrollButtonsPresenter scrollButtonsPresenter;

    [SerializeField] 
    private Transform _root;

    public void Initialize(IWeaponsSettingsProvider weaponsSettingsProvider, IWeaponsService weaponsService, IMoneyService moneyService)
    {
        var prefabs = InstantiateWeapons();
        scrollButtonsPresenter.Initialize(weaponsSettingsProvider, weaponsService, moneyService, prefabs);
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