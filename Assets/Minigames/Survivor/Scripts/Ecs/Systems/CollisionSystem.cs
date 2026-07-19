using System.Collections.Generic;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Player;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class CollisionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const float CellSize = 4f;

        private static readonly Vector2Int[] forwardOffsets =
        {
            new (-1, 1),
            new (0, 1),
            new (1, 1),
            new (1, 0)
        };

        private readonly EcsFilter<Position, BoundsComponent> filter;
        private readonly EcsFilter<PlayerTag> playerFilter;

        private readonly Dictionary<Vector2Int, List<int>> spatialGrid = new();

        private EcsEntity player;

        public void Init()
        {
            player = playerFilter.GetEntity(0);
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

                if (!spatialGrid.ContainsKey(key))
                {
                    spatialGrid.Add(key, new List<int>());
                }

                spatialGrid[key].Add(i);
            }

            foreach (var pair in spatialGrid)
            {
                foreach (var i in pair.Value)
                {
                    foreach (var j in pair.Value)
                    {
                        if (j <= i)
                        {
                            continue;
                        }

                        Resolve(i, j);
                    }
                }

                foreach (var offset in forwardOffsets)
                {
                    if (!spatialGrid.TryGetValue(pair.Key + offset, out var forwardCell))
                    {
                        continue;
                    }

                    foreach (var i in pair.Value)
                    {
                        foreach (var j in forwardCell)
                        {
                            Resolve(i, j);
                        }
                    }
                }
            }
        }

        private void Resolve(int i, int j)
        {
            ref var position1 = ref filter.Get1(i);
            var bounds1 = filter.Get2(i);

            ref var position2 = ref filter.Get1(j);
            var bounds2 = filter.Get2(j);

            var delta = position1.Value - position2.Value;
            var overlapX = bounds1.HalfSize.x + bounds2.HalfSize.x - Mathf.Abs(delta.x);
            var overlapY = bounds1.HalfSize.y + bounds2.HalfSize.y - Mathf.Abs(delta.y);

            if (overlapX <= 0 || overlapY <= 0)
            {
                return;
            }

            Vector2 translationVector;

            if (overlapX < overlapY)
            {
                translationVector = new Vector2(delta.x < 0 ? -overlapX : overlapX, 0);
            } else
            {
                translationVector = new Vector2(0, delta.y < 0 ? -overlapY : overlapY);
            }

            if (player == filter.GetEntity(i))
            {
                position2.Value -= translationVector;
            }
            else if (player == filter.GetEntity(j))
            {
                position1.Value += translationVector;
            }
            else
            {
                translationVector *= 0.5f;
                position1.Value += translationVector;
                position2.Value -= translationVector;
            }
        }
    }
}
