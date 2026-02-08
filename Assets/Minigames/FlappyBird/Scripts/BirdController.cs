using System;
using System.Threading;
using Core.Tools.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Minigames.FlappyBird.Scripts
{
    public class BirdController : IInitializable, IDisposable
    {
        private readonly SceneContainer sceneContainer;
        private readonly Bird bird;
        private readonly BirdConfig config;
        private readonly InputService inputService;

        private CancellationTokenSource tokenSource;
        private Vector3 velocity;

        public BirdController(SceneContainer sceneContainer, InputService inputService)
        {
            this.sceneContainer = sceneContainer;
            this.inputService = inputService;
            bird = sceneContainer.Bird;
            config = sceneContainer.BirdConfig;
        }

        void IInitializable.Initialize()
        {
            inputService.OnTapped += OnTap;
        }

        void IDisposable.Dispose()
        {
            tokenSource.CancelAndDispose();
            inputService.OnTapped -= OnTap;
        }
        
        public void Start()
        {
            tokenSource.CancelAndDispose();
            tokenSource = new CancellationTokenSource();
            bird.transform.position = Vector3.zero;
            velocity = Vector3.zero;
            
            MoveAsync(tokenSource.Token).Forget();
        }

        public void Stop()
        {
            tokenSource.CancelAndDispose();
        }

        private async UniTask MoveAsync(CancellationToken token)
        {
            var bottomPoint = sceneContainer.Camera.ScreenToWorldPoint(new Vector3());
            
            while (token.IsCancellationRequested is false && bird.gameObject)
            {
                var deltaTime = Time.deltaTime;

                if (bird.transform.position.y > bottomPoint.y)
                {
                    velocity += config.GravityAcceleration * deltaTime;
                }
                else
                {
                    velocity += -config.GravityAcceleration * deltaTime;
                }
                
                velocity.y = Mathf.Clamp(velocity.y, -config.MaxSpeed, config.MaxSpeed);
                bird.transform.position += velocity * deltaTime;

                var direction = velocity;
                direction.x = 3f;
                var directionNormalized = direction.normalized;
                var angle = Mathf.Atan2(directionNormalized.y, directionNormalized.x) * Mathf.Rad2Deg;
                var targetRotation = Quaternion.Euler(0f, 0f, angle);
                bird.transform.rotation = Quaternion.Lerp(bird.transform.rotation, targetRotation, deltaTime * 1f);

                
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }

        private void OnTap()
        {
            var topPoint = sceneContainer.Camera.ScreenToWorldPoint(new Vector3(0, Screen.height));
            var bottomPoint = sceneContainer.Camera.ScreenToWorldPoint(new Vector3());
            var birdPosition = bird.transform.position;
            
            if (birdPosition.y > topPoint.y || birdPosition.y < bottomPoint.y)
            {
                return;
            }
            
            velocity.y += config.AscendingSpeed;
        }
    }
}