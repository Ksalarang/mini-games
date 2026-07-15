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
        [Space]
        [SerializeField] private MoveConfig playerMoveConfig;
        [SerializeField] private SpriteAnimationConfig playerAnimationConfig; //todo: add a config container
        [SerializeField] private EnemySpawnConfig enemySpawnConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(sceneContainer);
            builder.RegisterComponent(sceneContainer.Camera);
            builder.RegisterComponent(sceneContainer.InfiniteFloor);
            builder.RegisterComponent(sceneContainer.PlayerContainer);
            builder.RegisterComponent(uiContainer);

            builder.RegisterComponent(playerMoveConfig);
            builder.RegisterComponent(playerAnimationConfig);
            builder.RegisterComponent(enemySpawnConfig);

            builder.Register<GameTimeService>(Lifetime.Singleton);

            builder.Register<PlayerInitSystem>(Lifetime.Singleton);
            builder.Register<TimerSystem>(Lifetime.Singleton);

            builder.Register<PlayerInputSystem>(Lifetime.Singleton);
            builder.Register<PlayerDirectionSystem>(Lifetime.Singleton);

            builder.Register<EnemySpawnSystem>(Lifetime.Singleton).WithParameter(sceneContainer.World);
            builder.Register<EnemyDirectionSystem>(Lifetime.Singleton);

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

            builder.RegisterEntryPoint<SelectionSceneLoadHandler>().WithParameter(uiContainer.BackButton.onClick);
        }
    }
}
