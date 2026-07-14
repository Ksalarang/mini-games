using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Player;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems.Player
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private readonly PlayerMoveConfig config;

        private readonly EcsFilter<PlayerTag> playerFilter;

        public PlayerMoveSystem(PlayerMoveConfig config)
        {
            this.config = config;
        }

        public void Run()
        {
            var player = playerFilter.GetEntity(0);
            ref var position = ref player.Get<Position>();
            var velocity = player.Get<Velocity>();
            var deltaTime = Time.deltaTime;
            position.X += velocity.X * deltaTime * config.Speed;
            position.Y += velocity.Y * deltaTime * config.Speed;
        }
    }
}
