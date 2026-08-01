using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs.Weapons;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;
using Minigames.Survivor.Scripts.Ecs.Components.Weapons;
using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class ProjectileSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld world;
        private readonly EcsFilter<ProjectileSpawnRequest, TimerExpiredEvent> filter;
        private readonly EcsFilter<PlayerTag> playerFilter;

        private readonly ObjectPool<GameObject> pool;

        private List<ProjectileWeapon> projectiles;

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

            projectiles = playerFilter.GetEntity(0).Get<WeaponInventory>().Projectiles;

            if (projectiles.Count > 0)
            {
                AddSpawnRequest(projectiles[0]);
            }
        }

        public void Run()
        {
            foreach (var i in filter)
            {
                var projectileType = filter.Get1(i).ProjectileType;

                foreach (var projectile in projectiles)
                {
                    if (projectile.Type == projectileType)
                    {
                        Spawn(projectile);
                        AddSpawnRequest(projectile);
                        break;
                    }
                }
            }
        }

        private void AddSpawnRequest(ProjectileWeapon projectile)
        {
            var entity = world.NewEntity();
            entity.Get<TimerComponent>().TimeLeft = projectile.Cooldown;
            entity.Get<ProjectileSpawnRequest>().ProjectileType = projectile.Type;
        }

        private void Spawn(ProjectileWeapon projectile)
        {
            var gameObject = pool.Get();
            var entity = world.NewEntity();
            entity.Get<ProjectileTag>();
            entity.Get<GameObjectComponent>().Value = gameObject;
            entity.Get<TransformComponent>().Value = gameObject.transform;

            ref var spriteRendererComponent = ref entity.Get<SpriteRendererComponent>();
            spriteRendererComponent.Value = gameObject.GetComponent<SpriteRenderer>();
            spriteRendererComponent.Value.sprite = projectile.Sprite;

            var player = playerFilter.GetEntity(0);
            entity.Get<Position>().Value = player.Get<Position>().Value;
            entity.Get<BoundsComponent>().HalfSize = spriteRendererComponent.Value.bounds.size * 0.5f;
            entity.Get<Speed>().Value = projectile.Speed;
            entity.Get<DamageComponent>().Value = projectile.Damage;
            entity.Get<ProjectileDirectionRequest>().DirectionType = projectile.DirectionType;
            entity.Get<RotationComponent>().RotateTowardsDirection = true;

            ref var timer = ref entity.Get<TimerComponent>();
            timer.TimeLeft = projectile.Lifetime;
        }
    }
}
