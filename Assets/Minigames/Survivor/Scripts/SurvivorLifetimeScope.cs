using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Ecs.Systems;
using Minigames.Survivor.Scripts.Ecs.Systems.Player;
using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Minigames.Survivor.Scripts
{
    public class SurvivorLifetimeScope : LifetimeScope
    {
        [SerializeField] private SurvivorSceneContainer sceneContainer;

        [Space]
        [SerializeField] private MoveConfig playerMoveConfig;
        [SerializeField] private SpriteAnimationConfig playerAnimationConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(sceneContainer);
            builder.RegisterComponent(sceneContainer.Camera);
            builder.RegisterComponent(sceneContainer.InfiniteFloor);
            builder.RegisterComponent(sceneContainer.PlayerContainer);

            builder.RegisterComponent(playerMoveConfig);
            builder.RegisterComponent(playerAnimationConfig);

            builder.Register<PlayerInitSystem>(Lifetime.Singleton);

            builder.Register<PlayerInputSystem>(Lifetime.Singleton);
            builder.Register<PlayerDirectionSystem>(Lifetime.Singleton);

            builder.Register<VelocitySystem>(Lifetime.Singleton);
            builder.Register<MoveSystem>(Lifetime.Singleton);
            builder.Register<TransformPositionSyncSystem>(Lifetime.Singleton);

            builder.Register<SpriteDirectionSystem>(Lifetime.Singleton);
            builder.Register<MoveStateSystem>(Lifetime.Singleton);
            builder.Register<AnimationSpriteSystem>(Lifetime.Singleton);
            builder.Register<SpriteAnimationSystem>(Lifetime.Singleton);

            builder.Register<CameraFollowSystem>(Lifetime.Singleton);
            builder.Register<CameraPositionSyncSystem>(Lifetime.Singleton);
            builder.Register<InfiniteFloorSystem>(Lifetime.Singleton);
        }
    }
}
