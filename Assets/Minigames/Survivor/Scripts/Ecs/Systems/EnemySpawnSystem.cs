using Core.Tools;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs.Enemies;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine;
using UnityEngine.Pool;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class EnemySpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly Camera camera;

        private readonly EcsWorld world;
        private readonly EcsFilter<DifficultyComponent> difficultyFilter;
        private readonly EcsFilter<EnemySpawnRequest, TimerExpiredEvent> spawnRequestFilter;
        private readonly EcsFilter<PlayerTag> playerFilter;
        private readonly EcsFilter<EnemyTag> enemyFilter;

        private readonly ObjectPool<GameObject> pool;

        public EnemySpawnSystem(EnemySpawnMasterConfig masterConfig, SurvivorSceneContainer sceneContainer)
        {
            camera = sceneContainer.Camera;

            pool = new ObjectPool<GameObject>(
                createFunc: () => Object.Instantiate(masterConfig.EnemyPrefab, sceneContainer.WorldContainer.Enemies),
                actionOnGet: go => go.SetActive(true),
                actionOnRelease: go => go.SetActive(false),
                actionOnDestroy: Object.Destroy,
                defaultCapacity: 100
            );
        }

        public void Init()
        {
            world.NewEntity().Get<EnemyPoolComponent>().Value = pool;
        }

        public void Run()
        {
            if (enemyFilter.GetEntitiesCount() >= difficultyFilter.Get1(0).TargetEnemySpawnRate)
            {
                return;
            }

            foreach (var i in spawnRequestFilter)
            {
                Spawn(spawnRequestFilter.Get1(i).Config);
            }
        }

        private void Spawn(EnemySpawnConfig config)
        {
            var enemy = pool.Get().GetComponent<Enemy>();
            var entity = world.NewEntity();

            entity.Get<EnemyTag>().Type = config.EnemyType;

            ref var position = ref entity.Get<Position>();
            position.Value = GetRandomPositionAroundPlayer(playerFilter.GetEntity(0).Get<Position>().Value);

            entity.Get<Speed>().Value = config.MoveSpeed;
            entity.Get<GameObjectComponent>().Value = enemy.gameObject;
            entity.Get<TransformComponent>().Value = enemy.Transform;

            ref var spriteRendererComponent = ref entity.Get<SpriteRendererComponent>();
            spriteRendererComponent.Value = enemy.SpriteRenderer;
            spriteRendererComponent.Value.sprite = config.Sprites[0];

            ref var spriteAnimationComponent = ref entity.Get<SpriteAnimationComponent>();
            spriteAnimationComponent.Sprites = config.Sprites;
            spriteAnimationComponent.FramesPerSecond = config.FramesPerSecond;

            entity.Get<BoundsComponent>().HalfSize = spriteRendererComponent.Value.bounds.size * 0.5f;
            entity.Get<RigidBodyComponent>();

            ref var health = ref entity.Get<Health>();
            health.Value = health.MaxValue = config.Health;
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
