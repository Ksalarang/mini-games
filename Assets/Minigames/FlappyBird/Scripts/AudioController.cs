using System;
using Core.Services.Audio;
using VContainer.Unity;

namespace Minigames.FlappyBird.Scripts
{
    public class AudioController : IInitializable, IDisposable
    {
        private readonly IAudioService audioService;
        private readonly InputService inputService;
        private readonly IPointProvider pointProvider;
        private readonly AudioConfig audioConfig;
        private readonly Bird bird;

        public AudioController(IAudioService audioService, IPointProvider pointProvider, InputService inputService,
            SceneContainer sceneContainer)
        {
            this.audioService = audioService;
            this.pointProvider = pointProvider;
            this.inputService = inputService;
            audioConfig = sceneContainer.AudioConfig;
            bird = sceneContainer.Bird;
        }

        void IInitializable.Initialize()
        {
            pointProvider.PointsChanged += OnPointsChanged;
            inputService.OnTapped += OnTapped;
            bird.OnCollisionEnter += OnCollisionEnter;
        }

        void IDisposable.Dispose()
        {
            pointProvider.PointsChanged -= OnPointsChanged;
            inputService.OnTapped -= OnTapped;
            bird.OnCollisionEnter -= OnCollisionEnter;
        }

        private void OnTapped()
        {
            audioService.PlaySound(audioConfig.Flap);
        }

        private void OnPointsChanged(int _)
        {
            audioService.PlaySound(audioConfig.Point);
        }

        private void OnCollisionEnter()
        {
            audioService.PlaySound(audioConfig.Hit);
        }
    }
}
