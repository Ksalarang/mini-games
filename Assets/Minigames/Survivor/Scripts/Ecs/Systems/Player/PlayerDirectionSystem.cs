using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Player;

namespace Minigames.Survivor.Scripts.Ecs.Systems.Player
{
    public class PlayerDirectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag> playerFilter;

        public void Run()
        {
            var player = playerFilter.GetEntity(0);
            ref var direction = ref player.Get<Direction>();
            var input = player.Get<PlayerMoveInput>();

            direction.Value.x = input.X;
            direction.Value.y = input.Y;
        }
    }
}
