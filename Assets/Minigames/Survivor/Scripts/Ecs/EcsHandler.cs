using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Systems;
using Minigames.Survivor.Scripts.Ecs.Systems.Player;
using UnityEngine;
using VContainer;

namespace Minigames.Survivor.Scripts.Ecs
{
    public class EcsHandler : MonoBehaviour
    {
        private IObjectResolver objectResolver;

        private EcsWorld world;
        private EcsSystems systems;

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
                .Add(objectResolver.Resolve<PlayerDirectionSystem>())
                .Add(objectResolver.Resolve<VelocitySystem>())
                .Add(objectResolver.Resolve<MoveSystem>())
                .Add(objectResolver.Resolve<TransformPositionSyncSystem>())
                .Add(objectResolver.Resolve<SpriteDirectionSystem>())
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
