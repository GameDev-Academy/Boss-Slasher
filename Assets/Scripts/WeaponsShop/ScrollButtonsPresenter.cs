using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using User;
using WeaponsSettings;

public class ScrollButtonsPresenter : MonoBehaviour
{
    public IReadOnlyReactiveProperty<int> CurrentWeaponIndex => _currentWeaponIndex;

    [SerializeField] 
    private Button _left;

    [SerializeField] 
    private Button _right;

    [SerializeField] 
    private TextMeshProUGUI _weaponCost;

    private IWeaponsSettingsProvider _weaponsSettingsProvider;

    private List<GameObject> _prefabs;
    private Dictionary<GameObject, string> _weapons;
    private ReactiveProperty<int> _currentWeaponIndex;

    public void Initialize(IWeaponsSettingsProvider weaponsSettingsProvider, IWeaponsService weaponsService, IMoneyService moneyService,
        List<GameObject> weapons)
    {
        _weaponsSettingsProvider = weaponsSettingsProvider;
        _prefabs = weapons;
        _weapons = new Dictionary<GameObject, string>();
        
        var weaponIds = _weaponsSettingsProvider.GetWeaponsId().ToList();
        for (var i = 0; i < _prefabs.Count; i++)
        {
            _weapons[_prefabs[i]] = weaponIds[i];
        }

        _currentWeaponIndex = new ReactiveProperty<int>(0);
        _currentWeaponIndex.Subscribe(_ => ChangeWeaponsCost());

        _right.OnClickAsObservable().Subscribe(_ => ScrollRight()).AddTo(this);
        _left.OnClickAsObservable().Subscribe(_ => ScrollLeft()).AddTo(this);
    }

    public string GetCurrentWeaponId()
    {
        var currentWeapon = _prefabs[_currentWeaponIndex.Value];
        return _weapons[currentWeapon];
    }

    private void ScrollLeft()
    {
        _prefabs[_currentWeaponIndex.Value].SetActive(false);

        if (_currentWeaponIndex.Value == 0)
        {
            _currentWeaponIndex.Value = _prefabs.Count - 1;
        }
        else
        {
            _currentWeaponIndex.Value--;
        }

        _prefabs[_currentWeaponIndex.Value].SetActive(true);
    }

    private void ScrollRight()
    {
        _prefabs[_currentWeaponIndex.Value].SetActive(false);

        if (_currentWeaponIndex.Value == _prefabs.Count - 1)
        {
            _currentWeaponIndex.Value = 0;
        }
        else
        {
            _currentWeaponIndex.Value++;
        }

        _prefabs[_currentWeaponIndex.Value].SetActive(true);
    }

    private void ChangeWeaponsCost()
    {
        var currentWeapon = _prefabs[_currentWeaponIndex.Value];
        var weaponsId = _weapons[currentWeapon];
        var weaponsCost = _weaponsSettingsProvider.GetCost(weaponsId);

        _weaponCost.text = weaponsCost.ToString();
    }
}