using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;
using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine.Pool;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class SpriteObjectDestroySystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<SpriteObjectPoolComponent> poolFilter;
        private readonly EcsFilter<SpriteObjectComponent, DestroyRequest> destroyFilter;

        private IObjectPool<SpriteObject> pool;

        public void Init()
        {
            pool = poolFilter.Get1(0).Value;
        }

        public void Run()
        {
            foreach (var i in destroyFilter)
            {
                pool.Release(destroyFilter.Get1(i).Value);
                destroyFilter.GetEntity(i).Destroy();
            }
        }
    }
}
