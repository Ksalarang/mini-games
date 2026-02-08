using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Core.Services
{
    public class AddressablesSceneService : ISceneService
    {
        private SceneInstance? currentScene;
        
        public async UniTask<SceneInstance> LoadSceneAsync(
            string key,
            LoadSceneMode mode = LoadSceneMode.Single,
            bool activateOnLoad = true)
        {
            if (currentScene.HasValue && currentScene.Value.Scene.isLoaded)
            {
                await Addressables.UnloadSceneAsync(currentScene.Value);
                
                currentScene = null;
            }
            
            var handle = Addressables.LoadSceneAsync(key, mode, activateOnLoad);
            await handle.ToUniTask();

            currentScene = handle.Result;
            return handle.Result;
        }
    }
}