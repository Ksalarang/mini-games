using System;
using System.Threading;
using Core.Services;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Core.MinigameSelectionScene
{
    public class MinigameSelectionPresenter : IAsyncStartable, IDisposable
    {
        private readonly MinigameSelectionConfig config;
        private readonly MinigameSelectionView minigameSelectionView;
        private readonly ISceneService sceneService;
        private readonly IAssetService assetService;

        public MinigameSelectionPresenter(MinigameSelectionConfig config,
            MinigameSelectionView minigameSelectionView,
            ISceneService sceneService,
            IAssetService assetService)
        {
            this.config = config;
            this.minigameSelectionView = minigameSelectionView;
            this.sceneService = sceneService;
            this.assetService = assetService;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            foreach (var minigameConfig in config.Minigames)
            {
                var minigameButtonView = await assetService.InstantiateAsync<MinigameButtonView>(
                    config.MinigameButtonViewReference,
                    minigameSelectionView.Container);

                minigameButtonView.Label.text = minigameConfig.Name;
                minigameButtonView.Button.image.sprite = minigameConfig.Icon;
                minigameButtonView.Button.onClick.AddListener(() =>
                {
                    minigameButtonView.Button.onClick.RemoveAllListeners();
                    sceneService.LoadSceneAsync(minigameConfig.SceneParams);
                });
            }
        }

        public void Dispose()
        {
            assetService.Release(config.MinigameButtonViewReference);
        }
    }
}
