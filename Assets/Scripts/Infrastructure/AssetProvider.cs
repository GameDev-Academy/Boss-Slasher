using ConfigurationProviders;
using UnityEngine;

public class AssetProvider : IAssetProvider
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }
}