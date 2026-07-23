using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.Ecs.Systems;
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

            systems.Add(objectResolver.Resolve<PlayerInitSystem>());
            systems.Add(objectResolver.Resolve<TimerSystem>());

            systems.Add(objectResolver.Resolve<PlayerInputSystem>());
            systems.Add(objectResolver.Resolve<PlayerDirectionSystem>());

            systems.Add(objectResolver.Resolve<ProjectileSpawnSystem>());

            systems.Add(objectResolver.Resolve<EnemySpawnSystem>());
            systems.Add(objectResolver.Resolve<EnemyDirectionSystem>());

            systems.Add(objectResolver.Resolve<SpatialGridSystem>());
            systems.Add(objectResolver.Resolve<AlignedBoxCollisionSystem>());
            systems.Add(objectResolver.Resolve<OrientedBoxCollisionSystem>());

            systems.Add(objectResolver.Resolve<VelocitySystem>());
            systems.Add(objectResolver.Resolve<MoveSystem>());
            systems.Add(objectResolver.Resolve<TransformPositionSyncSystem>());

            systems.Add(objectResolver.Resolve<RotateTowardsDirectionSystem>());
            systems.Add(objectResolver.Resolve<TransformRotationSyncSystem>());

            systems.Add(objectResolver.Resolve<EnemyContactDamageSystem>());
            systems.Add(objectResolver.Resolve<PlayerProjectileDamageSystem>());
            systems.Add(objectResolver.Resolve<DamageSystem>());
            systems.Add(objectResolver.Resolve<PlayerHealthBarSystem>());
            systems.Add(objectResolver.Resolve<EnemyReleaseSystem>());
            systems.Add(objectResolver.Resolve<ProjectileReleaseSystem>());

            systems.Add(objectResolver.Resolve<SpriteDirectionSystem>());
            systems.Add(objectResolver.Resolve<MoveStateSystem>());
            systems.Add(objectResolver.Resolve<AnimationSpriteSystem>());
            systems.Add(objectResolver.Resolve<SpriteAnimationSystem>());

            systems.OneFrame<TimerExpiredEvent>();
            systems.OneFrame<CollisionEvent>();
            systems.OneFrame<OrientedBoxCollisionEvent>();
            systems.OneFrame<DamageEvent>();

            systems.Init();

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
