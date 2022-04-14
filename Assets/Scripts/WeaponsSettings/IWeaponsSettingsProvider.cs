using System.Collections.Generic;

namespace WeaponsSettings
{
    public interface IWeaponsSettingsProvider
    {
        IEnumerable<string> GetWeaponsId();
        int GetCost(string id);
    }
}