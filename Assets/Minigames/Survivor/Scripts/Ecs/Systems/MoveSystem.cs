using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class MoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Position, Velocity> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var position = ref filter.Get1(i);
                var velocity = filter.Get2(i);

                position.Value += velocity.Value;
            }
        }
    }
}
