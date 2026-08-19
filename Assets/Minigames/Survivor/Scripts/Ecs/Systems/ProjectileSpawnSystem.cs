using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs.Weapons;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;
using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class ProjectileSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld world;
        private readonly EcsFilter<ProjectileSpawnRequest, TimerExpiredEvent> spawnFilter;
        private readonly EcsFilter<PlayerTag> playerFilter;
        private readonly EcsFilter<WeaponComponent> weaponFilter;

        private readonly ObjectPool<GameObject> pool;

        public ProjectileSpawnSystem(WeaponBundleConfig bundleConfig, SurvivorWorldContainer worldContainer)
        {
            pool = new ObjectPool<GameObject>(
                createFunc: () => Object.Instantiate(bundleConfig.WeaponPrefab, worldContainer.Projectiles),
                actionOnGet: go => go.SetActive(true),
                actionOnRelease: go => go.SetActive(false),
                actionOnDestroy: Object.Destroy,
                defaultCapacity: 100
            );
        }

        public void Init()
        {
            world.NewEntity().Get<ProjectilePoolComponent>().Value = pool;
            AddSpawnRequest(weaponFilter.GetEntity(0));
        }

        public void Run()
        {
            foreach (var i in spawnFilter)
            {
                var projectileId = spawnFilter.Get1(i).ProjectileId;

                foreach (var j in weaponFilter)
                {
                    if (weaponFilter.Get1(j).Id == projectileId)
                    {
                        var weapon = weaponFilter.GetEntity(j);
                        Spawn(weapon);
                        AddSpawnRequest(weapon);
                        break;
                    }
                }
            }
        }

        private void AddSpawnRequest(EcsEntity weapon)
        {
            var entity = world.NewEntity();
            entity.Get<TimerComponent>().TimeLeft = weapon.Get<CooldownComponent>().Value;
            entity.Get<ProjectileSpawnRequest>().ProjectileId = weapon.Get<WeaponComponent>().Id;
        }

        private void Spawn(EcsEntity weapon)
        {
            var gameObject = pool.Get();
            var projectile = world.NewEntity();
            projectile.Get<ProjectileTag>();
            projectile.Get<GameObjectComponent>().Value = gameObject;
            projectile.Get<TransformComponent>().Value = gameObject.transform;

            ref var spriteRendererComponent = ref projectile.Get<SpriteRendererComponent>();
            spriteRendererComponent.Value = gameObject.GetComponent<SpriteRenderer>();
            spriteRendererComponent.Value.sprite = weapon.Get<SpriteComponent>().Value;

            var player = playerFilter.GetEntity(0);
            projectile.Get<Position>().Value = player.Get<Position>().Value;
            projectile.Get<BoundsComponent>().HalfSize = spriteRendererComponent.Value.bounds.size * 0.5f;
            projectile.Get<Speed>().Value = weapon.Get<SpeedComponent>().Value;
            projectile.Get<DamageComponent>().Value = weapon.Get<DamageComponent>().Value;
            projectile.Get<ProjectileDirectionRequest>().TargetingType = weapon.Get<WeaponComponent>().TargetingType;
            projectile.Get<RotationComponent>().RotateTowardsDirection = true;

            ref var timer = ref projectile.Get<TimerComponent>();
            timer.TimeLeft = weapon.Get<LifetimeComponent>().Value;
        }
    }
}
