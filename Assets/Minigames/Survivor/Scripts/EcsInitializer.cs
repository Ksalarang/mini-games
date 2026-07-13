using Leopotam.Ecs;
using UnityEngine;

namespace Minigames.Survivor.Scripts
{
    public class EcsInitializer : MonoBehaviour
    {
        private EcsWorld world;
        private EcsSystems systems;

        private void Awake()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);
        }
    }
}
