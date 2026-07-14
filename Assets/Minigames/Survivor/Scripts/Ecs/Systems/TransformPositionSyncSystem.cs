using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class TransformPositionSyncSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Position, TransformComponent> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                var position = filter.Get1(i);
                ref var transformComponent = ref filter.Get2(i);
                transformComponent.Value.localPosition = position.Value;
            }
        }
    }
}
