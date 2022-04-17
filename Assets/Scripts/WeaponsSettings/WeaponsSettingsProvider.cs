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
        private Dictionary<string, int> _weaponIndexById;
        
        public void Initialize()
        {
            _weaponSettingsById = new ();
            _weaponIndexById = new Dictionary<string, int>();

            for (var i = 0; i < _weaponSettings.Length; i++)
            {
                var weaponSettings = _weaponSettings[i];
                if (_weaponSettingsById.ContainsKey(weaponSettings.Id))
                {
                    Debug.LogError($"Id {weaponSettings.Id} has already existed, " +
                                   "please check the weapons Id in weaponsSettingsProvider");
                    continue;
                }

                _weaponSettingsById[weaponSettings.Id] = weaponSettings;
                _weaponIndexById[weaponSettings.Id] = i;
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

        public GameObject GetPrefab(string id)
        {
            var weapon = GetSettings(id);
            return weapon.Prefab;
        }

        public int GetIndex(string id)
        {
            return _weaponIndexById[id];
        }

        private WeaponSettings GetSettings(string id)
        {
            Assert.IsTrue(_weaponSettingsById.ContainsKey(id), $"There is no attached weapon to id {id}");
            
            return _weaponSettingsById[id];
        }
    }
}