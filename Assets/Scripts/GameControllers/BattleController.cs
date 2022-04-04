using ConfigurationProviders;
using Unity.VisualScripting;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    private IConfigurationProvider _configurationProvider;
    
    public void Initialize(IConfigurationProvider configurationProvider)
    {
        _configurationProvider = configurationProvider;
    }
    //TODO: Внутри создаем по этому профилю - боевые характеристики
    // .. GetCharacteristic(Charactestics.Speed);
}