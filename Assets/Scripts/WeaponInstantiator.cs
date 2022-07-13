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
    
    private IWeaponProvider _weaponProvider;
    private IWeaponsSettingsProvider _weaponsSettingsProvider;
    private IWeaponsService _weaponsService;
    
    public void Awake()
    {
        var userProfileService = ServiceLocator.Instance.GetSingle<IUserProfileService>();
        _weaponProvider = userProfileService.GetProfile();
        _weaponsSettingsProvider = ServiceLocator.Instance.GetSingle<IWeaponsSettingsProvider>();
        _weaponsService = ServiceLocator.Instance.GetSingle<IWeaponsService>();
        
    }

    private void OnEnable()
    {
        var currentWeaponId = _weaponsService.GetCurrentSelectedWeaponIndex();
        //var currentWeaponId = _weaponProvider.CurrentWeapon;
        var currentWeaponIdStream = new ReactiveProperty<int>(_weaponsService.GetCurrentSelectedWeaponIndex());
        
        var currentWeaponSubscription = currentWeaponIdStream
            .Subscribe(x => InstantiateCurrentWeapon(x)).AddTo(this);
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
