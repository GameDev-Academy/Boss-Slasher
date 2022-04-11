using System.Collections.Generic;
using UnityEngine;

namespace WeaponsSettings
{
    [CreateAssetMenu(fileName = "WeaponsSettingsProvider", menuName = "WeaponsSettingsProvider")]
    public class WeaponsSettingsProvider : ScriptableObject, IWeaponsSettingsProvider
    {
        [SerializeField] 
        private WeaponSettings[] _weaponSettings;
        
        private Dictionary<string, WeaponSettings> _weaponSettingsById;
        
        public void Initialize()
        {
            _weaponSettingsById = new ();
        
            foreach (var weaponSettings in _weaponSettings)
            {
                if (_weaponSettingsById.ContainsKey(weaponSettings.Id))
                {
                    Debug.LogError($"Id {weaponSettings.Id} has already existed, " +
                                   "please check the weapons Id in weaponsSettingsProvider");
                }
                _weaponSettingsById[weaponSettings.Id] = weaponSettings;
            }
        }

        public WeaponSettings[] GetWeapons()
        {
            return _weaponSettings;
        }
        
        public WeaponSettings GetSettings(string id)
        {
            if (!_weaponSettingsById.ContainsKey(id))
            {
                Debug.LogError($"There is no attached weapon to id {id}");
            }
            
            return _weaponSettingsById[id];
        }

        public int GetCost(string id)
        {
            var weapon = GetSettings(id);
            return weapon.Cost;
        }
    }
}