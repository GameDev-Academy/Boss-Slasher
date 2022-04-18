using System.Collections.Generic;
using UnityEngine;

namespace WeaponsSettings
{
    public interface IWeaponsSettingsProvider
    {
        IEnumerable<string> GetWeaponsId();
        int GetCost(string id);
        int GetIndex(string id);
        GameObject GetPrefab(string id);
    }
}