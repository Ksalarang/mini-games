using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs.Weapons;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;
using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine.Pool;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class ProjectileSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld world;
        private readonly EcsFilter<WeaponSpawnRequest, TimerExpiredEvent> spawnFilter;
        private readonly EcsFilter<PlayerTag> playerFilter;
        private readonly EcsFilter<WeaponComponent> weaponFilter;
        private readonly EcsFilter<SpriteObjectPoolComponent> poolFilter;

        private IObjectPool<SpriteObject> pool;

        public void Init()
        {
            pool = poolFilter.Get1(0).Value;
        }

        public void Run()
        {
            foreach (var i in spawnFilter)
            {
                if (spawnFilter.Get1(i).WeaponType != WeaponType.Projectile)
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
            var spriteObject = pool.Get();
            var projectile = world.NewEntity();
            projectile.Get<ProjectileTag>();
            projectile.Get<SpriteObjectComponent>().Value = spriteObject;
            projectile.Get<TransformComponent>().Value = spriteObject.Transform;

            ref var spriteRendererComponent = ref projectile.Get<SpriteRendererComponent>();
            spriteRendererComponent.Value = spriteObject.SpriteRenderer;
            spriteRendererComponent.Value.sprite = weapon.Get<SpriteComponent>().Value;
            spriteRendererComponent.Value.sortingOrder = weapon.Get<RenderOrderComponent>().SortingOrder;

            projectile.Get<Position>().Value = playerFilter.GetEntity(0).Get<Position>().Value;
            projectile.Get<BoundsComponent>().HalfSize = spriteRendererComponent.Value.bounds.size * 0.5f;
            projectile.Get<SpeedComponent>().Value = weapon.Get<SpeedComponent>().Value;
            projectile.Get<DamageComponent>().Value = weapon.Get<DamageComponent>().Value;
            projectile.Get<ProjectileDirectionRequest>().TargetingType = weapon.Get<WeaponComponent>().TargetingType;
            projectile.Get<RotationComponent>().RotateTowardsDirection = true;

            ref var timer = ref projectile.Get<TimerComponent>();
            timer.TimeLeft = weapon.Get<LifetimeComponent>().Value;
        }
    }
}
