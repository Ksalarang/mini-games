using Core.Services.UnityAppEvents;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Minigames.FlappyBird.Scripts
{
    public class FlappyBirdLifetimeScope : LifetimeScope
    {
        [SerializeField] private SceneContainer sceneContainer;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(sceneContainer);

            builder.Register<InputService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<BirdController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<BirdSpriteController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<TowerGenerator>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<PointController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<PlayerDataStorage>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.RegisterEntryPoint<AppEventDispatchHandler>();
            builder.RegisterEntryPoint<GameFlow>();
            builder.RegisterEntryPoint<SelectionSceneLoadHandler>();
            builder.RegisterEntryPoint<AudioController>();
        }
    }
}