using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class RotateTowardsDirectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RotationComponent, DirectionComponent> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var rotation = ref filter.Get1(i);

                if (!rotation.RotateTowardsDirection)
                {
                    continue;
                }

                var direction = filter.Get2(i).Value;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                rotation.Angle = angle;
            }
        }
    }
}
