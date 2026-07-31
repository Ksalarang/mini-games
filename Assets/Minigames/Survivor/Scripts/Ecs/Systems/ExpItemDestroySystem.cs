using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;
using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine.Pool;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class ExpItemDestroySystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<ExpItemComponent, DestroyRequest> filter;
        private readonly EcsFilter<ExpItemPoolComponent> poolFilter;

        private IObjectPool<SpriteObject> pool;

        public void Init()
        {
            pool = poolFilter.Get1(0).Value;
        }

        public void Run()
        {
            foreach (var i in filter)
            {
                var entity = filter.GetEntity(i);
                pool.Release(entity.Get<SpriteObjectComponent>().Value);
                entity.Destroy();
            }
        }
    }
}
