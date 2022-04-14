using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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
                    continue;
                }
                
                _weaponSettingsById[weaponSettings.Id] = weaponSettings;
            }
        }

        public IEnumerable<string> GetWeaponsId()
        {
            foreach (var weaponSettings in _weaponSettings)
            {
                yield return weaponSettings.Id;
            }
        }

        public int GetCost(string id)
        {
            var weapon = GetSettings(id);
            return weapon.Cost;
        }

        private WeaponSettings GetSettings(string id)
        {
            Assert.IsTrue(_weaponSettingsById.ContainsKey(id), $"There is no attached weapon to id {id}");
            
            return _weaponSettingsById[id];
        }
    }
}