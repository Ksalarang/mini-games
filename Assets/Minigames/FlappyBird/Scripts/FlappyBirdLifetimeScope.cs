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
            builder.Register<TowerGenerator>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<PointController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.RegisterEntryPoint<GameFlow>();
            builder.RegisterEntryPoint<SelectionSceneLoadHandler>();
        }
    }
}