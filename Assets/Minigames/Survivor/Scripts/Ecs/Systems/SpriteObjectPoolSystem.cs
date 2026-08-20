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
                () => Object.Instantiate(worldContainer.SpriteObjectPrefab, worldContainer.Entities),
                so => so.gameObject.SetActive(true),
                so => so.gameObject.SetActive(false),
                so => Object.Destroy(so.gameObject),
                defaultCapacity: 200
            );
        }

        public void Init()
        {
            world.NewEntity().Get<SpriteObjectPoolComponent>().Value = pool;
        }
    }
}
