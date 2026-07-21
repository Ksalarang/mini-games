using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class TransformRotationSyncSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformComponent, RotationComponent> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var transform = ref filter.Get1(i);
                var rotation = filter.Get2(i);

                transform.Value.localEulerAngles = new Vector3(0f, 0f, rotation.Angle);
            }
        }
    }
}
