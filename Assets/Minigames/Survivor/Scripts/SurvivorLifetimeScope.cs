using Core.Common;
using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Ecs.Systems;
using Minigames.Survivor.Scripts.Ecs.Systems.Player;
using Minigames.Survivor.Scripts.SceneObjects;
using Minigames.Survivor.Scripts.Tools;
using Minigames.Survivor.Scripts.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Minigames.Survivor.Scripts
{
    public class SurvivorLifetimeScope : LifetimeScope
    {
        [SerializeField] private SurvivorSceneContainer sceneContainer;
        [SerializeField] private UiContainer uiContainer;
        [SerializeField] private SurvivorGameConfig gameConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(sceneContainer);
            builder.RegisterComponent(sceneContainer.Camera);
            builder.RegisterComponent(sceneContainer.InfiniteFloor);
            builder.RegisterComponent(sceneContainer.PlayerContainer);
            builder.RegisterComponent(uiContainer);

            builder.RegisterComponent(gameConfig);
            builder.RegisterComponent(gameConfig.PlayerConfig);
            builder.RegisterComponent(gameConfig.SpriteAnimationConfig);
            builder.RegisterComponent(gameConfig.EnemySpawnConfig);
            builder.RegisterComponent(gameConfig.EnemyDamageConfig);
            builder.RegisterComponent(gameConfig.WeaponConfig);

            builder.Register<GameTimeService>(Lifetime.Singleton);

            builder.Register<PlayerInitSystem>(Lifetime.Singleton);
            builder.Register<TimerSystem>(Lifetime.Singleton);

            builder.Register<PlayerInputSystem>(Lifetime.Singleton);
            builder.Register<PlayerDirectionSystem>(Lifetime.Singleton);

            builder.Register<ProjectileSpawnSystem>(Lifetime.Singleton);

            builder.Register<EnemySpawnSystem>(Lifetime.Singleton).WithParameter(sceneContainer.World);
            builder.Register<EnemyDirectionSystem>(Lifetime.Singleton);

            builder.Register<SpatialGridSystem>(Lifetime.Singleton);
            builder.Register<AlignedBoxCollisionSystem>(Lifetime.Singleton);
            builder.Register<OrientedBoxCollisionSystem>(Lifetime.Singleton);

            builder.Register<VelocitySystem>(Lifetime.Singleton);
            builder.Register<MoveSystem>(Lifetime.Singleton);
            builder.Register<TransformPositionSyncSystem>(Lifetime.Singleton);

            builder.Register<RotateTowardsDirectionSystem>(Lifetime.Singleton);
            builder.Register<TransformRotationSyncSystem>(Lifetime.Singleton);

            builder.Register<EnemyContactDamageSystem>(Lifetime.Singleton);
            builder.Register<DamageSystem>(Lifetime.Singleton);
            builder.Register<PlayerHealthBarSystem>(Lifetime.Singleton);

            builder.Register<SpriteDirectionSystem>(Lifetime.Singleton);
            builder.Register<MoveStateSystem>(Lifetime.Singleton);
            builder.Register<AnimationSpriteSystem>(Lifetime.Singleton);
            builder.Register<SpriteAnimationSystem>(Lifetime.Singleton);

            builder.Register<CameraFollowSystem>(Lifetime.Singleton);
            builder.Register<CameraPositionSyncSystem>(Lifetime.Singleton);
            builder.Register<InfiniteFloorSystem>(Lifetime.Singleton);

            builder.RegisterEntryPoint<SelectionSceneLoadHandler>().WithParameter(uiContainer.BackButton.onClick);
        }
    }
}
