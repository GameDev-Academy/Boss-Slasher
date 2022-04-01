using ConfigurationProviders;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    private IConfigurationProvider _configurationProvider;
    public void Initialize(IConfigurationProvider configurationProvider)
    {
        _configurationProvider = configurationProvider;
    }
    
    //TODO: Внутри создаем по этому профилю - какие-то боевые характеристики
    // .. GetCharacteristic(Charactestics.Speed);
}