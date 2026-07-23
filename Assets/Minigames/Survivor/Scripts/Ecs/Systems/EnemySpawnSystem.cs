using Core.Tools;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine;
using UnityEngine.Pool;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class EnemySpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EnemySpawnConfig config;
        private readonly Camera camera;

        private readonly EcsWorld world;
        private readonly EcsFilter<TimerComponent, EnemySpawnRequest, TimerExpiredEvent> spawnRequestFilter;
        private readonly EcsFilter<PlayerTag> playerFilter;

        private readonly ObjectPool<GameObject> pool;

        public EnemySpawnSystem(EnemySpawnConfig config, SurvivorSceneContainer sceneContainer)
        {
            this.config = config;
            camera = sceneContainer.Camera;

            pool = new ObjectPool<GameObject>(
                createFunc: () => Object.Instantiate(this.config.EnemyPrefab, sceneContainer.World),
                actionOnGet: go => go.SetActive(true),
                actionOnRelease: go => go.SetActive(false),
                actionOnDestroy: Object.Destroy,
                defaultCapacity: 100
            );
        }

        public void Init()
        {
            foreach (var data in config.Data)
            {
                AddSpawnRequest(data);
            }

            world.NewEntity().Get<EnemyPoolComponent>().Value = pool;
        }

        public void Run()
        {
            foreach (var i in spawnRequestFilter)
            {
                Spawn(spawnRequestFilter.Get2(i).Data);
            }
        }

        private void AddSpawnRequest(EnemySpawnData data)
        {
            var spawnEntity = world.NewEntity();
            ref var request = ref spawnEntity.Get<EnemySpawnRequest>();
            request.Data = data;

            ref var timer = ref spawnEntity.Get<TimerComponent>();
            timer.TimeLeft = data.SpawnIntervalSeconds;
            timer.Interval = data.SpawnIntervalSeconds;
        }

        private void Spawn(EnemySpawnData data)
        {
            var enemy = pool.Get().GetComponent<Enemy>();
            var entity = world.NewEntity();

            entity.Get<EnemyTag>().Type = data.EnemyType;

            ref var position = ref entity.Get<Position>();
            position.Value = GetRandomPositionAroundPlayer(playerFilter.GetEntity(0).Get<Position>().Value);

            entity.Get<Speed>().Value = data.MoveSpeed;
            entity.Get<GameObjectComponent>().Value = enemy.gameObject;
            entity.Get<TransformComponent>().Value = enemy.Transform;

            ref var spriteRendererComponent = ref entity.Get<SpriteRendererComponent>();
            spriteRendererComponent.Value = enemy.SpriteRenderer;
            spriteRendererComponent.Value.sprite = data.Sprites[0];

            ref var spriteAnimationComponent = ref entity.Get<SpriteAnimationComponent>();
            spriteAnimationComponent.Sprites = data.Sprites;
            spriteAnimationComponent.FramesPerSecond = data.FramesPerSecond;

            entity.Get<BoundsComponent>().HalfSize = spriteRendererComponent.Value.bounds.size * 0.5f;

            ref var health = ref entity.Get<Health>();
            health.Value = health.MaxValue = data.Health;
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
