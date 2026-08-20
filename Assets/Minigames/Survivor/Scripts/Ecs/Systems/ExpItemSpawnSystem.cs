using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine;
using UnityEngine.Pool;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class ExpItemSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ExpItemConfig config;

        private readonly EcsWorld world;
        private readonly EcsFilter<DeathEvent> filter;
        private readonly EcsFilter<SpriteObjectPoolComponent> poolFilter;

        private IObjectPool<SpriteObject> pool;

        public ExpItemSpawnSystem(ExpItemConfig config)
        {
            this.config = config;
        }

        public void Init()
        {
            pool = poolFilter.Get1(0).Value;
        }

        public void Run()
        {
            foreach (var i in filter)
            {
                var entity = filter.Get1(i).Entity;
                if (entity.Has<EnemyTag>())
                {
                    if (Random.value < config.SpawnChance)
                    {
                        SpawnExpItem(entity.Get<Position>().Value);
                    }
                }
            }
        }

        private void SpawnExpItem(Vector2 position)
        {
            var spriteObject = pool.Get();
            spriteObject.SpriteRenderer.sprite = config.Sprite;

            var entity = world.NewEntity();
            entity.Get<SpriteObjectComponent>().Value = spriteObject;
            entity.Get<TransformComponent>().Value = spriteObject.Transform;
            entity.Get<Position>().Value = position;
            entity.Get<SpriteRendererComponent>().Value = spriteObject.SpriteRenderer;
            entity.Get<BoundsComponent>().HalfSize = spriteObject.SpriteRenderer.bounds.size * 0.5f;
            entity.Get<ExpItemComponent>().Value = 1;
        }
    }
}
