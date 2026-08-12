using System;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;
using Minigames.Survivor.Scripts.Ecs.Systems;
using VContainer;
using VContainer.Unity;

namespace Minigames.Survivor.Scripts.Ecs
{
    public class EcsHandler : IEcsHandler, IStartable, ITickable, ILateTickable, IDisposable
    {
        private readonly IObjectResolver objectResolver;

        private EcsWorld world;
        private EcsSystems systems;
        private EcsSystems lateUpdateSystems;

        public bool Active { get; set; }

        public EcsHandler(IObjectResolver objectResolver)
        {
            this.objectResolver = objectResolver;
        }

        void IStartable.Start()
        {
            Initialize();
            Active = true;
        }

        void ITickable.Tick()
        {
            if (!Active)
            {
                return;
            }

            systems.Run();
        }

        void ILateTickable.LateTick()
        {
            if (!Active)
            {
                return;
            }

            lateUpdateSystems.Run();
        }

        void IDisposable.Dispose()
        {
            Destroy();
        }

        public void Initialize()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);
            lateUpdateSystems = new EcsSystems(world);

            systems.Add(objectResolver.Resolve<TimerSystem>());
            systems.Add(objectResolver.Resolve<SessionTimeSystem>());
            systems.Add(objectResolver.Resolve<DifficultySystem>());

            systems.Add(objectResolver.Resolve<PlayerInitSystem>());
            systems.Add(objectResolver.Resolve<PlayerInputSystem>());
            systems.Add(objectResolver.Resolve<PlayerDirectionSystem>());

            systems.Add(objectResolver.Resolve<ProjectileSpawnSystem>());

            systems.Add(objectResolver.Resolve<SmallEnemySpawnRequestSystem>());
            systems.Add(objectResolver.Resolve<EnemySpawnSystem>());
            systems.Add(objectResolver.Resolve<EnemySpawnRequestDeleteSystem>());
            systems.Add(objectResolver.Resolve<EnemyDirectionSystem>());

            systems.Add(objectResolver.Resolve<SpatialGridSystem>());
            systems.Add(objectResolver.Resolve<AlignedBoxCollisionSystem>());
            systems.Add(objectResolver.Resolve<OrientedBoxCollisionSystem>());

            systems.Add(objectResolver.Resolve<ProjectileDirectionSystem>());

            systems.Add(objectResolver.Resolve<VelocitySystem>());
            systems.Add(objectResolver.Resolve<MoveSystem>());

            systems.Add(objectResolver.Resolve<RotateTowardsDirectionSystem>());

            systems.Add(objectResolver.Resolve<EnemyContactDamageSystem>());
            systems.Add(objectResolver.Resolve<PlayerProjectileDamageSystem>());
            systems.Add(objectResolver.Resolve<DamageSystem>());
            systems.Add(objectResolver.Resolve<PlayerHealthBarSystem>());

            systems.Add(objectResolver.Resolve<ExpItemSpawnSystem>());
            systems.Add(objectResolver.Resolve<ExpSystem>());
            systems.Add(objectResolver.Resolve<UpgradeSystem>());
            systems.Add(objectResolver.Resolve<PlayerExpBarSystem>());

            systems.Add(objectResolver.Resolve<ExpItemDestroySystem>());
            systems.Add(objectResolver.Resolve<EnemyDestroySystem>());
            systems.Add(objectResolver.Resolve<ProjectileDestroySystem>());

            systems.Add(objectResolver.Resolve<TransformPositionSyncSystem>());
            systems.Add(objectResolver.Resolve<TransformRotationSyncSystem>());

            systems.Add(objectResolver.Resolve<SpriteDirectionSystem>());
            systems.Add(objectResolver.Resolve<MoveStateSystem>());
            systems.Add(objectResolver.Resolve<AnimationSpriteSystem>());
            systems.Add(objectResolver.Resolve<SpriteAnimationSystem>());

            systems.OneFrame<TimerExpiredEvent>();
            systems.OneFrame<CollisionEvent>();
            systems.OneFrame<OrientedBoxCollisionEvent>();
            systems.OneFrame<DamageEvent>();
            systems.OneFrame<DeathEvent>();
            systems.OneFrame<ProjectileDirectionRequest>();

            systems.Init();

            lateUpdateSystems.Add(objectResolver.Resolve<CameraFollowSystem>());
            lateUpdateSystems.Add(objectResolver.Resolve<CameraPositionSyncSystem>());
            lateUpdateSystems.Add(objectResolver.Resolve<InfiniteFloorSystem>());

            lateUpdateSystems.Add(objectResolver.Resolve<GameOverSystem>());
            lateUpdateSystems.Init();
        }

        public void Destroy()
        {
            systems.Destroy();
            lateUpdateSystems.Destroy();
            world.Destroy();
        }
    }
}
