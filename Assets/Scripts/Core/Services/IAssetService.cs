using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Services
{
    public interface IAssetService
    {
        UniTask<T> LoadAsync<T>(string key);
        UniTask<T> LoadAsync<T>(AssetReference reference);
        
        UniTask<GameObject> InstantiateAsync(string key, Transform parent);
        UniTask<GameObject> InstantiateAsync(AssetReference reference, Transform parent);
        UniTask<T> InstantiateAsync<T>(string key, Transform parent) where T : Component;
        UniTask<T> InstantiateAsync<T>(AssetReference reference, Transform parent) where T : Component;
        
        void Release(string key);
        void Release(AssetReference reference);
    }
}