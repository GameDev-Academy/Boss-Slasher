using UnityEngine;

public interface IAssetProvider : IService
{
    T LoadAsset<T>(string path) where T : Object;
}