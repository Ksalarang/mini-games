using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Components
{
    public struct SpatialGridComponent
    {
        public Dictionary<Vector2Int, List<EcsEntity>> SpatialGrid;
    }
}
