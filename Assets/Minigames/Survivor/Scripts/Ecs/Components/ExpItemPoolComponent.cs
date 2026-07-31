using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine.Pool;

namespace Minigames.Survivor.Scripts.Ecs.Components
{
    public struct ExpItemPoolComponent
    {
        public IObjectPool<SpriteObject> Value;
    }
}
