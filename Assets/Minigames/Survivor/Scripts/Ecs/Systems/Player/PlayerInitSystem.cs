using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Player;
using Minigames.Survivor.Scripts.SceneObjects;

namespace Minigames.Survivor.Scripts.Ecs.Systems.Player
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private readonly PlayerContainer playerContainer;
        private readonly PlayerConfig playerConfig;

        private readonly EcsWorld world;

        public PlayerInitSystem(PlayerContainer playerContainer, PlayerConfig playerConfig)
        {
            this.playerContainer = playerContainer;
            this.playerConfig = playerConfig;
        }

        public void Init()
        {
            var player = world.NewEntity();

            player.Get<PlayerTag>();
            player.Get<Position>();

            ref var speed = ref player.Get<Speed>();
            speed.Value = playerConfig.MoveSpeed;

            ref var transformComponent = ref player.Get<TransformComponent>();
            transformComponent.Value = playerContainer.Transform;

            ref var spriteRenderer = ref player.Get<SpriteRendererComponent>();
            spriteRenderer.Value = playerContainer.SpriteRenderer;

            ref var bounds = ref player.Get<BoundsComponent>();
            bounds.HalfSize = spriteRenderer.Value.bounds.size * 0.5f;

            player.Get<MoveStateComponent>();
            player.Get<SpriteAnimationComponent>();

            ref var health = ref player.Get<Health>();
            health.Value = playerConfig.Health;
            health.MaxValue = playerConfig.Health;
        }
    }
}
