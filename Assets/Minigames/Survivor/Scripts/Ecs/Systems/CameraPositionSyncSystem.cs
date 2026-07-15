using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class CameraPositionSyncSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CameraComponent, Position> filter;

        public void Run()
        {
            ref var camera = ref filter.Get1(0);
            var position = filter.Get2(0);

            camera.Transform.position =
                new Vector3(position.Value.x, position.Value.y, camera.Transform.position.z);
        }
    }
}
