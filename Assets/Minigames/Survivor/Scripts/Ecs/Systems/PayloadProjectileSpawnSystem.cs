using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs.Weapons;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;
using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class PayloadProjectileSpawnSystem : IEcsRunSystem
    {
        private readonly WeaponBundleConfig bundleConfig;
        private readonly SurvivorWorldContainer worldContainer;

        private readonly EcsWorld world;
        private readonly EcsFilter<WeaponSpawnRequest, TimerExpiredEvent> spawnFilter;
        private readonly EcsFilter<PlayerTag> playerFilter;
        private readonly EcsFilter<WeaponComponent> weaponFilter;

        public PayloadProjectileSpawnSystem(WeaponBundleConfig bundleConfig, SurvivorWorldContainer worldContainer)
        {
            this.bundleConfig = bundleConfig;
            this.worldContainer = worldContainer;
        }

        public void Run()
        {
            foreach (var i in spawnFilter)
            {
                if (spawnFilter.Get1(i).WeaponType != WeaponType.PayloadProjectile)
                {
                    continue;
                }

                var id = spawnFilter.Get1(i).WeaponId;

                foreach (var j in weaponFilter)
                {
                    if (weaponFilter.Get1(j).Id == id)
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
            var weaponComponent = weapon.Get<WeaponComponent>();
            entity.Get<TimerComponent>().TimeLeft = weapon.Get<CooldownComponent>().Value;
            entity.Get<WeaponSpawnRequest>() = new WeaponSpawnRequest
            {
                WeaponType = weaponComponent.Type,
                WeaponId = weaponComponent.Id,
            };
        }

        private void Spawn(EcsEntity weapon)
        {
            var gameObject = Object.Instantiate(bundleConfig.WeaponPrefab, worldContainer.Projectiles);
            var projectile = world.NewEntity();

            projectile.Get<PayloadProjectileTag>();
            projectile.Get<GameObjectComponent>().Value = gameObject;
            projectile.Get<TransformComponent>().Value = gameObject.transform;

            ref var spriteRendererComponent = ref projectile.Get<SpriteRendererComponent>();
            spriteRendererComponent.Value = gameObject.GetComponent<SpriteRenderer>();
            spriteRendererComponent.Value.sprite = weapon.Get<SpriteComponent>().Value;

            projectile.Get<Position>() = playerFilter.GetEntity(0).Get<Position>();
            projectile.Get<BoundsComponent>().HalfSize = spriteRendererComponent.Value.bounds.size * 0.5f;
            projectile.Get<SpeedComponent>() = weapon.Get<SpeedComponent>();
            projectile.Get<DamageComponent>() = weapon.Get<DamageComponent>();
            projectile.Get<ProjectileDirectionRequest>().TargetingType = weapon.Get<WeaponComponent>().TargetingType;

            ref var timer = ref projectile.Get<TimerComponent>();
            var lifetime = weapon.Get<LifetimeRangeComponent>().Value;
            timer.TimeLeft = Random.Range(lifetime.Min, lifetime.Max);

            projectile.Get<ImpactRadiusComponent>() = weapon.Get<ImpactRadiusComponent>();
        }
    }
}
