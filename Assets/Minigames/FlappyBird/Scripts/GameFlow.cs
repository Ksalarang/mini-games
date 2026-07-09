using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Minigames.FlappyBird.Scripts
{
    public class GameFlow : IAsyncStartable, IDisposable
    {
        private readonly InputService inputService;
        private readonly BirdController birdController;
        private readonly BirdSpriteController birdSpriteController;
        private readonly TowerGenerator towerGenerator;
        private readonly PointController pointController;
        private readonly PlayerDataStorage playerDataStorage;
        private readonly SceneContainer sceneContainer;

        public GameFlow(
            InputService inputService,
            BirdController birdController,
            BirdSpriteController birdSpriteController,
            TowerGenerator towerGenerator,
            PointController pointController,
            PlayerDataStorage playerDataStorage,
            SceneContainer sceneContainer)
        {
            this.inputService = inputService;
            this.birdController = birdController;
            this.birdSpriteController = birdSpriteController;
            this.towerGenerator = towerGenerator;
            this.pointController = pointController;
            this.playerDataStorage = playerDataStorage;
            this.sceneContainer = sceneContainer;
        }

        async UniTask IAsyncStartable.StartAsync(CancellationToken token)
        {
            await sceneContainer.StartScreen.StartButton.OnClickAsync(token);
            await sceneContainer.StartScreen.FadeOut();

            Start();
        }

        void IDisposable.Dispose()
        {
            sceneContainer.Bird.OnCollisionEnter -= OnCollisionEnter;
        }

        private void Start()
        {
            birdSpriteController.StartFlapping();
            sceneContainer.Bird.OnCollisionEnter += OnCollisionEnter;
            towerGenerator.Start();
            pointController.Reset();
            birdController.Start();
            inputService.Enable();
        }

        private void Stop()
        {
            sceneContainer.Bird.OnCollisionEnter -= OnCollisionEnter;
            inputService.Disable();
            birdController.Stop();
            towerGenerator.Stop();
        }

        private void OnCollisionEnter()
        {
            birdSpriteController.StopFlapping();

            var loseScreen = sceneContainer.LoseScreen;
            loseScreen.PreviousPoints.text = playerDataStorage.GetMaxPoints().ToString();
            loseScreen.Points.text = pointController.Points.ToString();
            loseScreen.FadeIn();
            loseScreen.RetryButton.onClick.AddListener(Retry);

            playerDataStorage.SetMaxPoints();
            Stop();
            return;

            void Retry()
            {
                loseScreen.RetryButton.onClick.RemoveListener(Retry);
                loseScreen.FadeOut();
                Start();
            }
        }
    }
}