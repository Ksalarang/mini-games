using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Ecs.Systems;
using Minigames.Survivor.Scripts.Ecs.Systems.Player;
using Minigames.Survivor.Scripts.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Minigames.Survivor.Scripts
{
    public class SurvivorLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerContainer playerContainer;

        [Space]
        [SerializeField] private MoveConfig playerMoveConfig;
        [SerializeField] private SpriteAnimationConfig playerAnimationConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PlayerInitSystem>(Lifetime.Singleton)
                .WithParameter(playerContainer).WithParameter(playerMoveConfig);
            builder.Register<PlayerInputSystem>(Lifetime.Singleton);
            builder.Register<PlayerDirectionSystem>(Lifetime.Singleton);

            builder.Register<VelocitySystem>(Lifetime.Singleton);
            builder.Register<MoveSystem>(Lifetime.Singleton);
            builder.Register<TransformPositionSyncSystem>(Lifetime.Singleton);

            builder.Register<SpriteDirectionSystem>(Lifetime.Singleton);
            builder.Register<MoveStateSystem>(Lifetime.Singleton);
            builder.Register<AnimationSpriteSystem>(Lifetime.Singleton).WithParameter(playerAnimationConfig);
            builder.Register<SpriteAnimationSystem>(Lifetime.Singleton);
        }
    }
}
