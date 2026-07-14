using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Systems;
using Minigames.Survivor.Scripts.Ecs.Systems.Player;
using UnityEngine;
using VContainer;

namespace Minigames.Survivor.Scripts
{
    public class EcsHandler : MonoBehaviour
    {
        private EcsWorld world;
        private EcsSystems systems;

        private IObjectResolver objectResolver;

        [Inject]
        public void Construct(IObjectResolver objectResolver)
        {
            this.objectResolver = objectResolver;
        }

        private void Start()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);

            systems
                .Add(objectResolver.Resolve<PlayerInitSystem>())
                .Add(objectResolver.Resolve<PlayerInputSystem>())
                .Add(objectResolver.Resolve<PlayerMoveSystem>())
                .Add(objectResolver.Resolve<TransformPositionSyncSystem>())
                .Init();
        }

        private void Update()
        {
            systems.Run();
        }

        private void OnDestroy()
        {
            systems.Destroy();
            world.Destroy();
        }
    }
}
