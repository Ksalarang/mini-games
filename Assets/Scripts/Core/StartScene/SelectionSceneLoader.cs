using System.Threading;
using Core.Services;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Core.StartScene
{
    public class SelectionSceneLoader : IAsyncStartable
    {
        private const string SelectionSceneKey = "MinigameSelectionScene";
        
        private readonly ISceneService sceneService;
        
        public SelectionSceneLoader(ISceneService sceneService)
        {
            this.sceneService = sceneService;
        }
        
        async UniTask IAsyncStartable.StartAsync(CancellationToken cancellation)
        {
            await sceneService.LoadSceneAsync(SelectionSceneKey);
        }

        public async UniTask LoadAsync()
        {
            await sceneService.LoadSceneAsync(SelectionSceneKey);
        }
    }
}