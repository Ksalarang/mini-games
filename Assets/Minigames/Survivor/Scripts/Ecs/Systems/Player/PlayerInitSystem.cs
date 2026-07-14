using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Player;
using Minigames.Survivor.Scripts.Player;

namespace Minigames.Survivor.Scripts.Ecs.Systems.Player
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private readonly PlayerContainer playerContainer;
        private readonly MoveConfig moveConfig;

        private readonly EcsWorld world;

        public PlayerInitSystem(PlayerContainer playerContainer, MoveConfig moveConfig)
        {
            this.playerContainer = playerContainer;
            this.moveConfig = moveConfig;
        }

        public void Init()
        {
            var player = world.NewEntity();

            player.Get<PlayerTag>();
            player.Get<Position>();

            ref var speed = ref player.Get<Speed>();
            speed.Value = moveConfig.Speed;

            ref var transformComponent = ref player.Get<TransformComponent>();
            transformComponent.Value = playerContainer.Transform;
        }
    }
}
