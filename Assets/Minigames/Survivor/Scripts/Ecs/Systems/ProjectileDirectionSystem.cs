using System;
using Core.Tools;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs.Weapons;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class ProjectileDirectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ProjectileDirectionRequest> requestFilter;
        private readonly EcsFilter<PlayerTag, Position> playerFilter;
        private readonly EcsFilter<SpatialGridComponent> spacialGridFilter;

        public void Run()
        {
            foreach (var i in requestFilter)
            {
                var projectile = requestFilter.GetEntity(i);

                switch (requestFilter.Get1(i).DirectionType)
                {
                    case ProjectileDirectionType.PlayerDirection:
                        DirectTowardsPlayerDirection(projectile);
                        break;
                    case ProjectileDirectionType.AutoTarget:
                        DirectTowardsClosestEnemy(projectile);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void DirectTowardsPlayerDirection(EcsEntity projectile)
        {
            var playerDirection = playerFilter.GetEntity(0).Get<DirectionComponent>();
            Vector2 value;

            if (playerDirection.Value.x != 0f || playerDirection.Value.y != 0f)
            {
                value = playerDirection.Value;
            }
            else if (playerDirection.PrevValue.x != 0f || playerDirection.PrevValue.y != 0f)
            {
                value = playerDirection.PrevValue;
            }
            else
            {
                value = new Vector2(1f, 0f);
            }

            projectile.Get<DirectionComponent>().Value = value;
        }

        private void DirectTowardsClosestEnemy(EcsEntity projectile)
        {
            var playerPosition = playerFilter.Get2(0).Value;
            var minDistance = float.MaxValue;
            var enemyPosition = Vector2.zero;
            var spatialGridComponent = spacialGridFilter.Get1(0);
            var v = playerPosition / spatialGridComponent.CellSize;
            var gridPosition = new Vector2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
            var targetFound = false;
            const int maxRing = 4;

            for (var ring = 0; ring <= maxRing; ring++)
            {
                for (var x = -ring; x <= ring; x++)
                {
                    for (var y = -ring; y <= ring; y++)
                    {
                        if (ring > 0 && Mathf.Abs(x) != ring && Mathf.Abs(y) != ring)
                        {
                            continue;
                        }

                        if (!spatialGridComponent.SpatialGrid.TryGetValue(gridPosition + new Vector2Int(x, y),
                                out var entities))
                        {
                            continue;
                        }

                        foreach (var entity in entities)
                        {
                            if (!entity.Has<EnemyTag>())
                            {
                                continue;
                            }

                            var position = entity.Get<Position>().Value;
                            var distance = MathTools.GetSquaredDistance(position, playerPosition);

                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                enemyPosition = position;
                                targetFound = true;
                            }
                        }
                    }
                }

                var nextRingMinDistance = ring * spatialGridComponent.CellSize;

                if (targetFound && nextRingMinDistance * nextRingMinDistance >= minDistance)
                {
                    break;
                }
            }

            projectile.Get<DirectionComponent>().Value = targetFound
                ? MathTools.GetDirectionToTarget(enemyPosition, playerPosition)
                : Vector2.right;
        }
    }
}
