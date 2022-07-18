using UniRx;
using UnityEngine;
using User;

namespace WeaponsSettings
{
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

            weaponsService.CurrentWeapon.
                Subscribe(InstantiateCurrentWeapon).
                AddTo(this);
        }


        private void InstantiateCurrentWeapon(string currentWeaponId)
        {
            foreach (Transform child in _root)
            {
                Destroy(child.gameObject);
            }

            var weaponPrefab = _weaponsSettingsProvider.GetPrefab(currentWeaponId);
            var weaponInstance = Instantiate(weaponPrefab, _root);
            weaponInstance.SetActive(true);
        }
    }
}