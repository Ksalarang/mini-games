using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
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
        private EcsSystems lateUpdateSystems;

        [Inject]
        public void Construct(IObjectResolver objectResolver)
        {
            this.objectResolver = objectResolver;
        }

        private void Start()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world, "Systems");
            lateUpdateSystems = new EcsSystems(world, "LateUpdateSystems");

            systems
                .Add(objectResolver.Resolve<PlayerInitSystem>())
                .Add(objectResolver.Resolve<TimerSystem>())

                .Add(objectResolver.Resolve<PlayerInputSystem>())
                .Add(objectResolver.Resolve<PlayerDirectionSystem>())

                .Add(objectResolver.Resolve<ProjectileSpawnSystem>())

                .Add(objectResolver.Resolve<EnemySpawnSystem>())
                .Add(objectResolver.Resolve<EnemyDirectionSystem>())

                .Add(objectResolver.Resolve<SpatialGridSystem>())
                .Add(objectResolver.Resolve<AlignedBoxCollisionSystem>())
                .Add(objectResolver.Resolve<OrientedBoxCollisionSystem>())

                .Add(objectResolver.Resolve<VelocitySystem>())
                .Add(objectResolver.Resolve<MoveSystem>())
                .Add(objectResolver.Resolve<TransformPositionSyncSystem>())

                .Add(objectResolver.Resolve<RotateTowardsDirectionSystem>())
                .Add(objectResolver.Resolve<TransformRotationSyncSystem>())

                .Add(objectResolver.Resolve<EnemyContactDamageSystem>())
                .Add(objectResolver.Resolve<DamageSystem>())
                .Add(objectResolver.Resolve<PlayerHealthBarSystem>())

                .Add(objectResolver.Resolve<SpriteDirectionSystem>())
                .Add(objectResolver.Resolve<MoveStateSystem>())
                .Add(objectResolver.Resolve<AnimationSpriteSystem>())
                .Add(objectResolver.Resolve<SpriteAnimationSystem>())

                .OneFrame<TimerExpiredEvent>()
                .OneFrame<CollisionEvent>()
                .OneFrame<DamageEvent>()

                .Init();

            lateUpdateSystems
                .Add(objectResolver.Resolve<CameraFollowSystem>())
                .Add(objectResolver.Resolve<CameraPositionSyncSystem>())
                .Add(objectResolver.Resolve<InfiniteFloorSystem>())
                .Init();
        }

        private void Update()
        {
            systems.Run();
        }

        private void LateUpdate()
        {
            lateUpdateSystems.Run();
        }

        private void OnDestroy()
        {
            systems.Destroy();
            lateUpdateSystems.Destroy();
            world.Destroy();
        }
    }
}
