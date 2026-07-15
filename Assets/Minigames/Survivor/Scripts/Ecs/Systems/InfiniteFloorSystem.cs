using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class InfiniteFloorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly InfiniteFloor infiniteFloor;

        private readonly EcsWorld world;
        private readonly EcsFilter<CameraComponent, Position> cameraFilter;
        private readonly EcsFilter<FloorComponent> floorFilter;

        public InfiniteFloorSystem(InfiniteFloor infiniteFloor)
        {
            this.infiniteFloor = infiniteFloor;
        }

        public void Init()
        {
            var entity = world.NewEntity();
            ref var floor = ref entity.Get<FloorComponent>();
            floor.Transform = infiniteFloor.transform;
            floor.Material = infiniteFloor.MeshRenderer.material;
            floor.TextureWorldSize = infiniteFloor.TextureWorldSize;
            floor.Margin = infiniteFloor.Margin;

            var camera = cameraFilter.Get1(0);
            var height = camera.Value.orthographicSize * 2f + floor.Margin;
            var width = height * camera.Value.aspect + floor.Margin;

            floor.Transform.localScale = new Vector3(width, height, 1f);
            floor.Material.mainTextureScale = new Vector2(width, height) / floor.TextureWorldSize;
        }

        public void Run()
        {
            ref var floor = ref floorFilter.Get1(0);
            var cameraPosition = cameraFilter.Get2(0);

            floor.Material.mainTextureOffset = cameraPosition.Value / floor.TextureWorldSize;
        }
    }
}
