using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class SpriteDirectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SpriteRendererComponent, DirectionComponent> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var renderer = ref filter.Get1(i);
                var direction = filter.Get2(i);

                if (direction.Value.x > 0)
                {
                    renderer.Value.flipX = false;
                }
                else if (direction.Value.x < 0)
                {
                    renderer.Value.flipX = true;
                }
            }
        }
    }
}
