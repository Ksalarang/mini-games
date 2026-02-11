using System;
using System.Collections.Generic;
using System.Threading;
using Core.Services;
using Core.Tools.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Minigames.FlappyBird.Scripts
{
    public class TowerGenerator : ITowerProvider, IDisposable
    {
        private readonly IAssetService assetService;
        private readonly SceneContainer sceneContainer;
        private readonly TowerConfig config;
        private readonly List<Tower> towers = new();

        private CancellationTokenSource tokenSource;
        private Vector3 screenBottomLeft;
        private Vector3 screenTopRight;
        private float towerGap;
        private float yOffset;

        public IReadOnlyList<Tower> CurrentTowers => towers;

        public TowerGenerator(IAssetService assetService, SceneContainer sceneContainer)
        {
            this.assetService = assetService;
            this.sceneContainer = sceneContainer;
            config = sceneContainer.TowerConfig;
        }

        void IDisposable.Dispose()
        {
            assetService.Release(sceneContainer.TowerPrefabReference);
            tokenSource.CancelAndDispose();
        }

        public void Start()
        {
            tokenSource.CancelAndDispose();
            tokenSource = new CancellationTokenSource();
            towers.ForEach(t => t.DestroyGameObject());
            towers.Clear();
            GenerateTowersInLoopAsync(tokenSource.Token).Forget();
        }

        public void Stop()
        {
            tokenSource.CancelAndDispose();
        }

        private async UniTask GenerateTowersInLoopAsync(CancellationToken token)
        {
            while (token.IsCancellationRequested is false)
            {
                await CreateTowersAsync(token);

                if (token.IsCancellationRequested)
                {
                    break;
                }
                
                var delay = Random.Range(config.GenerationDelay.Min, config.GenerationDelay.Max);
                await UniTask.WaitForSeconds(delay, cancellationToken: token);
            }
        }

        private async UniTask CreateTowersAsync(CancellationToken token)
        {
            screenBottomLeft = sceneContainer.Camera.ScreenToWorldPoint(new Vector3(0, 0));
            screenTopRight = sceneContainer.Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            towerGap = Random.Range(config.TowerGap.Min, config.TowerGap.Max);
            
            var maxYOffset = sceneContainer.ReferenceTower.SpriteRenderer.bounds.size.y * 2 + towerGap
                             - (screenTopRight.y - screenBottomLeft.y);
            
            yOffset = -Random.Range(0, maxYOffset);
            
            await CreateTowerAsync(true, token);

            if (token.IsCancellationRequested)
            {
                return;
            }
            
            await CreateTowerAsync(false, token);
        }

        private async UniTask CreateTowerAsync(bool isBottom, CancellationToken token)
        {
            var tower = await assetService.InstantiateAsync<Tower>(sceneContainer.TowerPrefabReference,
                sceneContainer.WorldTransform);
            
            towers.Add(tower);

            if (token.IsCancellationRequested)
            {
                return;
            }
            
            var position = Vector3.zero;
            position.x = screenTopRight.x + tower.SpriteRenderer.bounds.size.x / 2;

            if (isBottom)
            {
                position.y = screenBottomLeft.y + tower.SpriteRenderer.bounds.size.y / 2;
            }
            else
            {
                position.y = screenBottomLeft.y + tower.SpriteRenderer.bounds.size.y * 1.5f + towerGap;
            }
            
            position.y += yOffset;

            tower.transform.rotation = Quaternion.Euler(0f, 0f, isBottom ? 0f : 180f);
            tower.transform.position = position;
            tower.IsBottom = isBottom;
            
            MoveTowerAsync(tower, token).Forget();
        }

        private async UniTask MoveTowerAsync(Tower tower, CancellationToken token)
        {
            while (tower
                   && tower.transform.position.x + tower.SpriteRenderer.bounds.size.x / 2 > screenBottomLeft.x
                   && token.IsCancellationRequested is false)
            {
                tower.transform.position += new Vector3(-config.TowerSpeed * Time.deltaTime, 0);
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }

            if (token.IsCancellationRequested)
            {
                return;
            }
            
            Object.Destroy(tower.gameObject);
            towers.Remove(tower);
        }
    }
}