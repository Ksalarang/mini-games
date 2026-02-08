using Core.MinigameSelectionScene;
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
    }
}