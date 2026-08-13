using Core.Common;
using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Ecs;
using Minigames.Survivor.Scripts.Ecs.Systems;
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
            builder.RegisterComponent(sceneContainer.WorldContainer);
            builder.RegisterComponent(sceneContainer.PlayerContainer);

            builder.RegisterComponent(uiContainer);
            builder.RegisterComponent(uiContainer.UpgradeCardSelectionView);

            builder.RegisterComponent(gameConfig);
            builder.RegisterComponent(gameConfig.PlayerConfig);
            builder.RegisterComponent(gameConfig.SpriteAnimationConfig);
            builder.RegisterComponent(gameConfig.EnemySpawnMasterConfig);
            builder.RegisterComponent(gameConfig.EnemyDamageConfig);
            builder.RegisterComponent(gameConfig.WeaponBundleConfig);
            builder.RegisterComponent(gameConfig.ExpItemConfig);
            builder.RegisterComponent(gameConfig.UpgradeBundleConfig);
            builder.RegisterComponent(gameConfig.DifficultyConfig);

            builder.Register<GameTimeService>(Lifetime.Singleton);

            builder.Register<TimerSystem>(Lifetime.Transient);
            builder.Register<SessionTimeSystem>(Lifetime.Transient);
            builder.Register<DifficultySystem>(Lifetime.Transient);

            builder.Register<PlayerInitSystem>(Lifetime.Transient);
            builder.Register<PlayerInputSystem>(Lifetime.Transient);
            builder.Register<PlayerDirectionSystem>(Lifetime.Transient);

            builder.Register<ProjectileSpawnSystem>(Lifetime.Transient);
            builder.Register<ProjectileDirectionSystem>(Lifetime.Transient);

            builder.Register<SmallEnemySpawnRequestSystem>(Lifetime.Transient);
            builder.Register<MediumEnemySpawnRequestSystem>(Lifetime.Transient);
            builder.Register<EnemySpawnSystem>(Lifetime.Transient);
            builder.Register<EnemySpawnRequestDeleteSystem>(Lifetime.Transient);
            builder.Register<EnemyDirectionSystem>(Lifetime.Transient);

            builder.Register<SpatialGridSystem>(Lifetime.Transient);
            builder.Register<AlignedBoxCollisionSystem>(Lifetime.Transient);
            builder.Register<OrientedBoxCollisionSystem>(Lifetime.Transient);

            builder.Register<VelocitySystem>(Lifetime.Transient);
            builder.Register<MoveSystem>(Lifetime.Transient);

            builder.Register<RotateTowardsDirectionSystem>(Lifetime.Transient);

            builder.Register<EnemyContactDamageSystem>(Lifetime.Transient);
            builder.Register<PlayerProjectileDamageSystem>(Lifetime.Transient);
            builder.Register<DamageSystem>(Lifetime.Transient);
            builder.Register<PlayerHealthBarSystem>(Lifetime.Transient);

            builder.Register<ExpItemSpawnSystem>(Lifetime.Transient);
            builder.Register<ExpSystem>(Lifetime.Transient);
            builder.Register<UpgradeSystem>(Lifetime.Transient);
            builder.Register<PlayerExpBarSystem>(Lifetime.Transient);

            builder.Register<ExpItemDestroySystem>(Lifetime.Transient);
            builder.Register<EnemyDestroySystem>(Lifetime.Transient);
            builder.Register<ProjectileDestroySystem>(Lifetime.Transient);

            builder.Register<TransformPositionSyncSystem>(Lifetime.Transient);
            builder.Register<TransformRotationSyncSystem>(Lifetime.Transient);

            builder.Register<SpriteDirectionSystem>(Lifetime.Transient);
            builder.Register<MoveStateSystem>(Lifetime.Transient);
            builder.Register<AnimationSpriteSystem>(Lifetime.Transient);
            builder.Register<SpriteAnimationSystem>(Lifetime.Transient);

            builder.Register<CameraFollowSystem>(Lifetime.Transient);
            builder.Register<CameraPositionSyncSystem>(Lifetime.Transient);
            builder.Register<InfiniteFloorSystem>(Lifetime.Transient);

            builder.Register<GameOverSystem>(Lifetime.Transient);

            builder.RegisterEntryPoint<EcsHandler>().As<IEcsHandler>();
            builder.RegisterEntryPoint<SelectionSceneLoadHandler>().WithParameter(uiContainer.BackButton.onClick);
        }
    }
}
