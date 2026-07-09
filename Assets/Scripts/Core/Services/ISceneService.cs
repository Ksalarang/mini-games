using Core.Scenes;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Core.Services
{
    public interface ISceneService
    {
        UniTask<SceneInstance> LoadSceneAsync(
            string key,
            LoadSceneMode mode = LoadSceneMode.Single,
            bool activateOnLoad = true
        );

        UniTask<SceneInstance> LoadSceneAsync(
            SceneParams sceneParams,
            LoadSceneMode mode = LoadSceneMode.Single,
            bool activateOnLoad = true
        );
    }
}
