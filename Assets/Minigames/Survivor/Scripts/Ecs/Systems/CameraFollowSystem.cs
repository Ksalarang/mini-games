using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class CameraFollowSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly Camera camera;

        private readonly EcsWorld world;
        private readonly EcsFilter<CameraComponent, Position> cameraFilter;
        private readonly EcsFilter<PlayerTag, Position> playerFilter;

        public CameraFollowSystem(Camera camera)
        {
            this.camera = camera;
        }

        public void Init()
        {
            var entity = world.NewEntity();

            ref var cameraComponent = ref entity.Get<CameraComponent>();
            cameraComponent.Value = camera;
            cameraComponent.Transform = camera.transform;

            entity.Get<Position>();
        }

        public void Run()
        {
            ref var cameraPosition = ref cameraFilter.Get2(0);
            var playerPosition = playerFilter.Get2(0);

            cameraPosition.Value = playerPosition.Value;
        }
    }
}
