using ConfigurationProviders;
using UnityEngine;

public class MetaGameController : MonoBehaviour
{
    private IConfigurationProvider _configurationProvider;
    
    public void Initialize(IConfigurationProvider configurationProvider)
    {
        _configurationProvider = configurationProvider;
    }
}