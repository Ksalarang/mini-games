using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine.Pool;

namespace Minigames.Survivor.Scripts.Ecs.Components
{
    public struct SpriteObjectPoolComponent
    {
        public IObjectPool<SpriteObject> Value;
    }
}
