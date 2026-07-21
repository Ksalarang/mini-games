using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class OrientedBoxCollisionSystem : IEcsRunSystem
    {
        private static readonly Vector2Int[] forwardOffsets =
        {
            new (-1, 1),
            new (0, 1),
            new (1, 1),
            new (1, 0)
        };

        private readonly EcsWorld world;
        private readonly EcsFilter<SpatialGridComponent> spatialGridFilter;

        private readonly Vector2[] separatingAxes = new Vector2[4];

        public void Run()
        {
            var spatialGrid = spatialGridFilter.Get1(0).SpatialGrid;

            foreach (var pair in spatialGrid)
            {
                var cell = pair.Value;

                for (var i = 0; i < cell.Count; i++)
                {
                    for (var j = i + 1; j < cell.Count; j++)
                    {
                        Resolve(cell[i], cell[j]);
                    }
                }

                foreach (var offset in forwardOffsets)
                {
                    if (!spatialGrid.TryGetValue(pair.Key + offset, out var forwardCell))
                    {
                        continue;
                    }

                    foreach (var entity1 in cell)
                    {
                        foreach (var entity2 in forwardCell)
                        {
                            Resolve(entity1, entity2);
                        }
                    }
                }
            }
        }

        private void Resolve(EcsEntity entity1, EcsEntity entity2)
        {
            if (!entity1.Has<RotationComponent>() && !entity2.Has<RotationComponent>())
            {
                return;
            }

            var bounds1 = entity1.Get<BoundsComponent>();
            var bounds2 = entity2.Get<BoundsComponent>();

            GetAxes(entity1, out var axisX1, out var axisY1);
            GetAxes(entity2, out var axisX2, out var axisY2);

            separatingAxes[0] = axisX1;
            separatingAxes[1] = axisY1;
            separatingAxes[2] = axisX2;
            separatingAxes[3] = axisY2;

            var delta = entity2.Get<Position>().Value - entity1.Get<Position>().Value;

            foreach (var axis in separatingAxes)
            {
                var distance = Mathf.Abs(Vector2.Dot(delta, axis));

                var reach1 = bounds1.HalfSize.x * Mathf.Abs(Vector2.Dot(axisX1, axis))
                             + bounds1.HalfSize.y * Mathf.Abs(Vector2.Dot(axisY1, axis));

                var reach2 = bounds2.HalfSize.x * Mathf.Abs(Vector2.Dot(axisX2, axis))
                             + bounds2.HalfSize.y * Mathf.Abs(Vector2.Dot(axisY2, axis));

                if (distance > reach1 + reach2)
                {
                    return;
                }
            }

            world.NewEntity().Get<CollisionEvent>() = new CollisionEvent
            {
                Entity1 = entity1,
                Entity2 = entity2,
            };
        }

        private static void GetAxes(EcsEntity entity, out Vector2 axisX, out Vector2 axisY)
        {
            var angle = entity.Has<RotationComponent>() ? entity.Get<RotationComponent>().Angle : 0f;
            var radians = angle * Mathf.Deg2Rad;

            axisX = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            axisY = new Vector2(-axisX.y, axisX.x);
        }
    }
}
