using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine;
using UnityEngine.Pool;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class SpriteObjectPoolSystem : IEcsInitSystem
    {
        private readonly EcsWorld world;
        private readonly ObjectPool<SpriteObject> pool;

        public SpriteObjectPoolSystem(SurvivorWorldContainer worldContainer)
        {
            pool = new ObjectPool<SpriteObject>(
                createFunc: () => Object.Instantiate(worldContainer.SpriteObjectPrefab, worldContainer.Entities),
                actionOnGet: so =>
                {
                    so.Transform.localEulerAngles = Vector3.zero;
                    so.gameObject.SetActive(true);
                },
                actionOnRelease: so => so.gameObject.SetActive(false),
                actionOnDestroy: so => Object.Destroy(so.gameObject),
                defaultCapacity: 200
            );
        }

        public void Init()
        {
            world.NewEntity().Get<SpriteObjectPoolComponent>().Value = pool;
        }
    }
}
