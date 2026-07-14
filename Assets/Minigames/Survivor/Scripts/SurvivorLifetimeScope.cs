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
        [SerializeField] private PlayerMoveConfig playerMoveConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PlayerInitSystem>(Lifetime.Singleton).WithParameter(playerContainer);
            builder.Register<PlayerInputSystem>(Lifetime.Singleton);
            builder.Register<PlayerMoveSystem>(Lifetime.Singleton).WithParameter(playerMoveConfig);
            builder.Register<TransformPositionSyncSystem>(Lifetime.Singleton);
        }
    }
}
