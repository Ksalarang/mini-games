using Core.Scenes;
using Cysharp.Threading.Tasks;
using UnityEngine;
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

        public async UniTask<SceneInstance> LoadSceneAsync(
            SceneParams sceneParams,
            LoadSceneMode mode = LoadSceneMode.Single,
            bool activateOnLoad = true)
        {
            var handle = Addressables.LoadSceneAsync(SceneNames.LoadingScene, mode, activateOnLoad);
            await handle.Task;

            var orientationParams = sceneParams.OrientationParams;
            Screen.orientation = orientationParams.Orientation;
            Screen.autorotateToPortrait = orientationParams.AutoRotateToPortrait;
            Screen.autorotateToPortraitUpsideDown = orientationParams.AutoRotateToPortraitUpsideDown;
            Screen.autorotateToLandscapeLeft = orientationParams.AutoRotateToLandscapeLeft;
            Screen.autorotateToLandscapeRight = orientationParams.AutoRotateToLandscapeRight;

            handle = Addressables.LoadSceneAsync(sceneParams.Key, mode, activateOnLoad);
            await handle.Task;

            return handle.Result;
        }
    }
}