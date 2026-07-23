using UnityEngine;
using UnityEngine.Pool;

namespace Minigames.Survivor.Scripts.Ecs.Components
{
    public struct ProjectilePoolComponent
    {
        public IObjectPool<GameObject> Value;
    }
}
