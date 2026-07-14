using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Player;
using Minigames.Survivor.Scripts.Player;

namespace Minigames.Survivor.Scripts.Ecs.Systems.Player
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private readonly PlayerContainer playerContainer;

        private readonly EcsWorld world;

        public PlayerInitSystem(PlayerContainer playerContainer)
        {
            this.playerContainer = playerContainer;
        }

        public void Init()
        {
            var player = world.NewEntity();
            player.Get<PlayerTag>();
            ref var transformComponent = ref player.Get<TransformComponent>();
            transformComponent.Value = playerContainer.Transform;
        }
    }
}
