using System;
using VContainer.Unity;

namespace Minigames.FlappyBird.Scripts
{
    public class GameFlow : IStartable, IDisposable
    {
        private readonly InputService inputService;
        private readonly BirdController birdController;
        private readonly TowerGenerator towerGenerator;
        private readonly SceneContainer sceneContainer;

        public GameFlow(
            InputService inputService,
            BirdController birdController,
            TowerGenerator towerGenerator,
            SceneContainer sceneContainer)
        {
            this.inputService = inputService;
            this.birdController = birdController;
            this.towerGenerator = towerGenerator;
            this.sceneContainer = sceneContainer;
        }

        void IStartable.Start()
        {
            Start();
        }

        void IDisposable.Dispose()
        {
            sceneContainer.Bird.OnCollisionEnter -= OnCollisionEnter;
        }

        private void Start()
        {
            sceneContainer.Bird.OnCollisionEnter += OnCollisionEnter;
            towerGenerator.Start();
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
            Stop();
            
            var loseScreen = sceneContainer.LoseScreen;
            loseScreen.FadeIn();
            loseScreen.RetryButton.onClick.AddListener(Retry);

            void Retry()
            {
                loseScreen.RetryButton.onClick.RemoveListener(Retry);
                loseScreen.FadeOut();
                Start();
            }
        }
    }
}