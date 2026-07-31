using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class AlignedBoxCollisionSystem : IEcsInitSystem, IEcsRunSystem
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
        private readonly EcsFilter<PlayerTag> playerFilter;

        private EcsEntity player;

        public void Init()
        {
            player = playerFilter.GetEntity(0);
        }

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
            if (entity1.Has<RotationComponent>() || entity2.Has<RotationComponent>())
            {
                return;
            }

            ref var position1 = ref entity1.Get<Position>();
            var bounds1 = entity1.Get<BoundsComponent>();

            ref var position2 = ref entity2.Get<Position>();
            var bounds2 = entity2.Get<BoundsComponent>();

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

            if (entity1.Has<RigidBodyComponent>() && entity2.Has<RigidBodyComponent>())
            {
                if (player == entity1)
                {
                    position2.Value -= translationVector;
                }
                else if (player == entity2)
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

            world.NewEntity().Get<CollisionEvent>() = new CollisionEvent
            {
                Entity1 = entity1,
                Entity2 = entity2,
            };
        }
    }
}
