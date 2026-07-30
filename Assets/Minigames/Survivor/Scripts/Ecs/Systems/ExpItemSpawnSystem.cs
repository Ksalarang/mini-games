using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine;
using UnityEngine.Pool;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class ExpItemSpawnSystem : IEcsRunSystem
    {
        private readonly ExpItemConfig config;

        private readonly EcsWorld world;
        private readonly EcsFilter<DeathEvent> filter;

        private readonly ObjectPool<SpriteObject> pool;

        public ExpItemSpawnSystem(ExpItemConfig config, SurvivorWorldContainer worldContainer)
        {
            this.config = config;

            pool = new ObjectPool<SpriteObject>(
                () =>
                {
                    var so = Object.Instantiate(config.Prefab, worldContainer.ExpItems);
                    so.SpriteRenderer.sprite = config.Sprite;
                    return so;
                },
                so => so.gameObject.SetActive(true),
                so => so.gameObject.SetActive(false),
                so => Object.Destroy(so.gameObject),
                defaultCapacity: 100
            );
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

            var entity = world.NewEntity();
            entity.Get<GameObjectComponent>().Value = spriteObject.gameObject;
            entity.Get<TransformComponent>().Value = spriteObject.Transform;
            entity.Get<Position>().Value = position;
            entity.Get<SpriteRendererComponent>().Value = spriteObject.SpriteRenderer;
            entity.Get<BoundsComponent>().HalfSize = spriteObject.SpriteRenderer.bounds.size * 0.5f;
            entity.Get<ExpItemComponent>().Value = 1;
            entity.Get<NonRigidComponent>();
        }
    }
}
