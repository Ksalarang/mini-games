using Core.Tools;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Player;
using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class EnemySpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EnemySpawnConfig config;
        private readonly Camera camera;
        private readonly Transform worldTransform;

        private readonly EcsWorld world;
        private readonly EcsFilter<Timer, EnemySpawnRequest, TimerExpired> spawnRequestFilter;
        private readonly EcsFilter<PlayerTag> playerFilter;

        public EnemySpawnSystem(EnemySpawnConfig config, SurvivorSceneContainer sceneContainer)
        {
            this.config = config;
            worldTransform = sceneContainer.World;
            camera = sceneContainer.Camera;
        }

        public void Init()
        {
            foreach (var data in config.Data)
            {
                AddSpawnRequest(data);
            }
        }

        public void Run()
        {
            foreach (var i in spawnRequestFilter)
            {
                Spawn(spawnRequestFilter.Get2(i).EnemyType);
            }
        }

        private void AddSpawnRequest(EnemySpawnData data)
        {
            var spawnEntity = world.NewEntity();
            ref var request = ref spawnEntity.Get<EnemySpawnRequest>();
            request.EnemyType = data.EnemyType;

            ref var timer = ref spawnEntity.Get<Timer>();
            timer.TimeLeft = data.SpawnIntervalSeconds;
            timer.Interval = data.SpawnIntervalSeconds;
        }

        private void Spawn(EnemyType enemyType)
        {
            var data = config.GetData(enemyType)!;
            var enemy = Object.Instantiate(config.EnemyPrefab, worldTransform).GetComponent<Enemy>();

            var entity = world.NewEntity();
            ref var tag = ref entity.Get<EnemyTag>();
            tag.Type = enemyType;

            ref var position = ref entity.Get<Position>();
            position.Value = GetRandomPositionAroundPlayer(playerFilter.GetEntity(0).Get<Position>().Value);

            ref var speed = ref entity.Get<Speed>();
            speed.Value = data.MoveSpeed;

            ref var transformComponent = ref entity.Get<TransformComponent>();
            transformComponent.Value = enemy.Transform;

            ref var spriteRendererComponent = ref entity.Get<SpriteRendererComponent>();
            spriteRendererComponent.Value = enemy.SpriteRenderer;
            spriteRendererComponent.Value.sprite = data.Sprites[0];

            ref var spriteAnimationComponent = ref entity.Get<SpriteAnimationComponent>();
            spriteAnimationComponent.Sprites = data.Sprites;
            spriteAnimationComponent.FramesPerSecond = data.FramesPerSecond;

            ref var bounds = ref entity.Get<BoundsComponent>();
            bounds.HalfSize = spriteRendererComponent.Value.bounds.size * 0.5f;

            entity.Get<Health>();
        }

        private Vector2 GetRandomPositionAroundPlayer(Vector2 playerPosition)
        {
            var cameraHeight = camera.orthographicSize * 2f;
            var cameraWidth = cameraHeight * camera.aspect;
            var radius = Mathf.Max(cameraHeight, cameraWidth) / 2f + 1f;

            return MathTools.GetRandomPointOnCircle(playerPosition, radius);
        }
    }
}
