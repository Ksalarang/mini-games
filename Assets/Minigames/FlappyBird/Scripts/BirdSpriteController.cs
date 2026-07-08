using System;
using System.Threading;
using Core.Tools.Extensions;
using Cysharp.Threading.Tasks;

namespace Minigames.FlappyBird.Scripts
{
    public class BirdSpriteController : IDisposable
    {
        private readonly Bird bird;
        private readonly BirdSpriteConfig config;

        private CancellationTokenSource tokenSource;
        private int flapSpriteIndex;

        public BirdSpriteController(SceneContainer sceneContainer)
        {
            bird = sceneContainer.Bird;
            config = sceneContainer.BirdSpriteConfig;
        }

        void IDisposable.Dispose()
        {
            tokenSource.CancelAndDispose();
        }

        public void StartFlapping()
        {
            tokenSource.CancelAndDispose();
            tokenSource = new CancellationTokenSource();
            FlapAsync(tokenSource.Token).Forget();
        }

        public void StopFlapping()
        {
            tokenSource.CancelAndDispose();
        }

        private async UniTask FlapAsync(CancellationToken token)
        {
            while (token.IsCancellationRequested is false)
            {
                await UniTask.Delay(config.FlapUpdatePeriodMillis, cancellationToken: token);

                if (token.IsCancellationRequested)
                {
                    break;
                }

                var index = flapSpriteIndex++ % config.FlappingSprites.Length;
                bird.SpriteRenderer.sprite = config.FlappingSprites[index];
            }
        }
    }
}
