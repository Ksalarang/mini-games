using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;
using Minigames.Survivor.Scripts.SceneObjects;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private readonly PlayerContainer playerContainer;
        private readonly PlayerConfig playerConfig;
        private readonly WeaponConfig weaponConfig;

        private readonly EcsWorld world;

        public PlayerInitSystem(PlayerContainer playerContainer, PlayerConfig playerConfig, WeaponConfig weaponConfig)
        {
            this.playerContainer = playerContainer;
            this.playerConfig = playerConfig;
            this.weaponConfig = weaponConfig;
        }

        public void Init()
        {
            var player = world.NewEntity();

            player.Get<PlayerTag>();
            player.Get<Position>();
            player.Get<Speed>().Value = playerConfig.MoveSpeed;
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

            player.Get<PlayerExpComponent>().NextLevelValue = 10;

            AddSpawnRequest(weaponConfig.Projectiles[0]);
        }

        private void AddSpawnRequest(ProjectileData data)
        {
            var entity = world.NewEntity();

            ref var timer = ref entity.Get<TimerComponent>();
            timer.TimeLeft = data.Cooldown;
            timer.Interval = data.Cooldown;

            ref var request = ref entity.Get<ProjectileSpawnRequest>();
            request.Data = data;
        }
    }
}
