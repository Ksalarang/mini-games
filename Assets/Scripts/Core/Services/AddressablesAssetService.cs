using System.Collections.Generic;
using Core.Tools.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.Services
{
    public class AddressablesAssetService : IAssetService
    {
        private readonly Dictionary<object, AsyncOperationHandle> handles = new();

        public async UniTask<T> LoadAsync<T>(string key)
        {
            if (handles.TryGetValue(key, out var loadedHandle))
            {
                return (T)loadedHandle.Result;
            }
            
            var handle = Addressables.LoadAssetAsync<T>(key);
            await handle.ToUniTask();
            
            handles.Add(key, handle);
            
            return handle.Result;
        }

        public async UniTask<T> LoadAsync<T>(AssetReference reference)
        {
            if (handles.TryGetValue(reference, out var loadedHandle))
            {
                return (T)loadedHandle.Result;
            }
            
            var handle = reference.LoadAssetAsync<T>();
            await handle.ToUniTask();

            handles.Add(reference, handle);
            
            return handle.Result;
        }

        public async UniTask<GameObject> InstantiateAsync(string key, Transform parent)
        {
            var prefab = await LoadAsync<GameObject>(key);
            
            return Object.Instantiate(prefab, parent);
        }

        public async UniTask<GameObject> InstantiateAsync(AssetReference reference, Transform parent)
        {
            var prefab = await LoadAsync<GameObject>(reference);
            
            return Object.Instantiate(prefab, parent);
        }
        
        public async UniTask<T> InstantiateAsync<T>(string key, Transform parent)
            where T : Component
        {
            var gameObject = await InstantiateAsync(key, parent);
            return gameObject.GetComponent<T>();
        }

        public async UniTask<T> InstantiateAsync<T>(AssetReference reference, Transform parent)
            where T : Component
        {
            var gameObject = await InstantiateAsync(reference, parent);
            return gameObject.GetComponent<T>();
        }

        public void Release(string key)
        {
            if (key.IsNullOrEmpty())
            {
                return;
            }

            if (handles.TryGetValue(key, out var handle))
            {
                handle.Release();
                handles.Remove(key);
            }
        }

        public void Release(AssetReference reference)
        {
            if (reference == null || !reference.IsValid())
            {
                return;
            }

            if (handles.ContainsKey(reference))
            {
                reference.ReleaseAsset();
                handles.Remove(reference);
            }
        }
    }
}