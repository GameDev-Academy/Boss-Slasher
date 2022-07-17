using System.Linq;
using UniRx;
using UnityEngine;
using User;
using WeaponsSettings;


/// <summary>
/// Instantiate prefab of current weapon in weapon root position 
/// </summary>
public class WeaponInstantiator : MonoBehaviour
{
    [SerializeField] private Transform _root;

    private IWeaponsSettingsProvider _weaponsSettingsProvider;

    public void Awake()
    {
        _weaponsSettingsProvider = ServiceLocator.Instance.GetSingle<IWeaponsSettingsProvider>();
        var weaponsService = ServiceLocator.Instance.GetSingle<IWeaponsService>();
        var userProfile = ServiceLocator.Instance.GetSingle<IUserProfileService>();
        var weaponProvider = userProfile.GetCurrentProfile();

        weaponProvider.CurrentWeapon.Subscribe(_ =>
        {
            InstantiateCurrentWeapon(weaponsService.GetCurrentSelectedWeaponIndex());
        }).AddTo(this);
    }


    private void InstantiateCurrentWeapon(int currentWeaponId)
    {
        var currentWeaponName = _weaponsSettingsProvider.GetWeaponsId().ToList()[currentWeaponId];

        foreach (Transform child in _root)
        {
            Destroy(child.gameObject);
        }

        var weaponPrefab = Instantiate(_weaponsSettingsProvider.GetPrefab(currentWeaponName), _root);
        weaponPrefab.SetActive(true);
    }
}