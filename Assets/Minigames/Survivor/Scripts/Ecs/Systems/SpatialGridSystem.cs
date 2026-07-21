using System.Collections.Generic;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class SpatialGridSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const float CellSize = 4f;

        private readonly EcsWorld world;
        private readonly EcsFilter<Position, BoundsComponent> filter;

        private readonly Dictionary<Vector2Int, List<int>> spatialGrid = new();

        public void Init()
        {
            world.NewEntity().Get<SpatialGridComponent>().SpatialGrid = spatialGrid;
        }

        public void Run()
        {
            foreach (var bucket in spatialGrid.Values)
            {
                bucket.Clear();
            }

            foreach (var i in filter)
            {
                var position = filter.Get1(i);
                var v = position.Value / CellSize;
                var key = new Vector2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));

                if (!spatialGrid.TryGetValue(key, out var bucket))
                {
                    bucket = new List<int>();
                    spatialGrid.Add(key, bucket);
                }

                bucket.Add(i);
            }
        }
    }
}
