using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Core.Services
{
    public class AddressablesSceneService : ISceneService
    {
        public async UniTask<SceneInstance> LoadSceneAsync(
            string key,
            LoadSceneMode mode = LoadSceneMode.Single,
            bool activateOnLoad = true)
        {
            var handle = Addressables.LoadSceneAsync(key, mode, activateOnLoad);
            await handle.Task;

            return handle.Result;
        }
    }
}