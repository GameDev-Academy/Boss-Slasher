using System.Collections.Generic;
using UnityEngine;

namespace WeaponsSettings
{
    [CreateAssetMenu(fileName = "WeaponsSettingsProvider", menuName = "WeaponsSettingsProvider")]
    public class WeaponsSettingsProvider : ScriptableObject
    {
        [SerializeField] 
        private WeaponSettings[] _weaponSettings;
        
        private Dictionary<string, WeaponSettings> _weaponSettingsById;
        
        public void Initialize()
        {
            _weaponSettingsById = new ();
        
            foreach (var weaponSetting in _weaponSettings)
            {
                _weaponSettingsById[weaponSetting.Id] = weaponSetting;
            }
        }
        
        public WeaponSettings GetWeapon(string id)
        {
            if (!_weaponSettingsById.ContainsKey(id))
            {
                Debug.LogError($"There is no attached weapon to id {id}");
            }
            
            return _weaponSettingsById[id];
        }

        public WeaponSettings[] GetWeapons()
        {
            return _weaponSettings;
        }
    }
}