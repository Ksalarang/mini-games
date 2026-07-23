using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class PlayerDirectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag> playerFilter;

        public void Run()
        {
            var player = playerFilter.GetEntity(0);
            ref var direction = ref player.Get<DirectionComponent>();
            var input = player.Get<PlayerMoveInput>();

            if (direction.Value.x != 0f || direction.Value.y != 0f)
            {
                direction.PrevValue = direction.Value;
            }

            direction.Value.x = input.X;
            direction.Value.y = input.Y;
        }
    }
}
