using ConfigurationProviders;
using UnityEngine;

public interface IAssetProvider : IService
{
    T Load<T>(string path) where T : Object;
}