using System.Collections.Generic;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Configs.Weapons;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;
using Minigames.Survivor.Scripts.SceneObjects;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private readonly PlayerContainer playerContainer;
        private readonly PlayerConfig playerConfig;
        private readonly WeaponBundleConfig weaponBundleConfig;

        private readonly EcsWorld world;

        public PlayerInitSystem(PlayerContainer playerContainer, PlayerConfig playerConfig, WeaponBundleConfig weaponBundleConfig)
        {
            this.playerContainer = playerContainer;
            this.playerConfig = playerConfig;
            this.weaponBundleConfig = weaponBundleConfig;
        }

        public void Init()
        {
            var player = world.NewEntity();

            player.Get<PlayerTag>();
            player.Get<Position>();
            player.Get<SpeedComponent>().Value = playerConfig.MoveSpeed;
            player.Get<GameObjectComponent>().Value = playerContainer.gameObject;
            player.Get<TransformComponent>().Value = playerContainer.Transform;

            ref var spriteRenderer = ref player.Get<SpriteRendererComponent>();
            spriteRenderer.Value = playerContainer.SpriteRenderer;

            player.Get<BoundsComponent>().HalfSize = spriteRenderer.Value.bounds.size * 0.5f;
            player.Get<RigidBodyComponent>();
            player.Get<MoveStateComponent>();
            player.Get<SpriteAnimationComponent>();

            ref var health = ref player.Get<Health>();
            health.Value = playerConfig.Health;
            health.MaxValue = playerConfig.Health;

            ref var weaponInventory = ref player.Get<WeaponInventory>();
            weaponInventory.Weapons = new List<EcsEntity>();

            var weaponEntity = world.NewEntity();
            weaponBundleConfig.StartingWeapon.AddComponentsTo(ref weaponEntity);
            weaponInventory.Weapons.Add(weaponEntity);
            AddSpawnRequest(weaponEntity);
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
    }
}
