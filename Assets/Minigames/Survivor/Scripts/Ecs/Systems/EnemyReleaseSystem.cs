using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;
using UnityEngine.Pool;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class EnemyReleaseSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld world;
        private readonly EcsFilter<EnemyTag> enemyFilter;
        private readonly EcsFilter<EnemyPoolComponent> poolFilter;

        private IObjectPool<GameObject> pool;

        public void Init()
        {
            pool = poolFilter.Get1(0).Value;
        }

        public void Run()
        {
            foreach (var i in enemyFilter)
            {
                var entity = enemyFilter.GetEntity(i);
                var health = entity.Get<Health>();

                if (health.Value > 0f)
                {
                    continue;
                }

                pool.Release(entity.Get<GameObjectComponent>().Value);
                entity.Destroy();
            }
        }
    }
}
