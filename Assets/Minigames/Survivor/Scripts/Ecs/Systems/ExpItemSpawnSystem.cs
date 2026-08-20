using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Configs.Enemies;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class ExpItemSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ExpItemConfig config;
        private readonly EnemySpawnMasterConfig enemySpawnMasterConfig;

        private readonly EcsWorld world;
        private readonly EcsFilter<DeathEvent> deathFilter;
        private readonly EcsFilter<SpriteObjectPoolComponent> poolFilter;

        private IObjectPool<SpriteObject> pool;

        public ExpItemSpawnSystem(ExpItemConfig config, EnemySpawnMasterConfig enemySpawnMasterConfig)
        {
            this.config = config;
            this.enemySpawnMasterConfig = enemySpawnMasterConfig;
        }

        public void Init()
        {
            pool = poolFilter.Get1(0).Value;
        }

        public void Run()
        {
            foreach (var i in deathFilter)
            {
                var entity = deathFilter.Get1(i).Entity;

                if (entity.Has<EnemyTag>())
                {
                    if (Random.value < config.SpawnChance)
                    {
                        SpawnExpItem(entity.Get<Position>().Value, entity);
                    }
                }
            }
        }

        private void SpawnExpItem(Vector2 position, EcsEntity enemy)
        {
            var spriteObject = pool.Get();
            var enemyTag = enemy.Get<EnemyTag>();
            spriteObject.SpriteRenderer.sprite = config.Sprites[(int)enemyTag.Type];

            var entity = world.NewEntity();
            entity.Get<SpriteObjectComponent>().Value = spriteObject;
            entity.Get<TransformComponent>().Value = spriteObject.Transform;
            entity.Get<Position>().Value = position;
            entity.Get<SpriteRendererComponent>().Value = spriteObject.SpriteRenderer;
            entity.Get<BoundsComponent>().HalfSize = spriteObject.SpriteRenderer.bounds.size * 0.5f;

            var enemyHealth = enemySpawnMasterConfig.GetConfig(enemyTag.Type, enemyTag.Id).Health;
            entity.Get<ExpItemComponent>().Value = Mathf.Max(1, (int)enemyHealth / 10);
        }
    }
}
