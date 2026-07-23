using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using UnityEngine;
using UnityEngine.Pool;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class ProjectileReleaseSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<ProjectilePoolComponent> projectilePoolFilter;
        private readonly EcsFilter<ProjectileTag> projectileFilter;
        private readonly EcsFilter<ProjectileTag, TimerExpiredEvent> timerExpiredFilter;

        private IObjectPool<GameObject> pool;

        public void Init()
        {
            pool = projectilePoolFilter.Get1(0).Value;
        }

        public void Run()
        {
            foreach (var i in projectileFilter)
            {
                if (projectileFilter.Get1(i).Destroy)
                {
                    var entity = projectileFilter.GetEntity(i);
                    pool.Release(entity.Get<GameObjectComponent>().Value);
                    entity.Destroy();
                }
            }

            foreach (var i in timerExpiredFilter)
            {
                var entity = timerExpiredFilter.GetEntity(i);
                pool.Release(entity.Get<GameObjectComponent>().Value);
                entity.Destroy();
            }
        }
    }
}
