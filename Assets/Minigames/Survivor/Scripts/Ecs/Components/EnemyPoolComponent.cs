using UnityEngine;
using UnityEngine.Pool;

namespace Minigames.Survivor.Scripts.Ecs.Components
{
    public struct EnemyPoolComponent
    {
        public IObjectPool<GameObject> Value;
    }
}
