using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Player;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class CollisionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Position, SpriteRendererComponent> filter;
        private readonly EcsFilter<PlayerTag> playerFilter;

        public void Run()
        {
            var player = playerFilter.GetEntity(0);

            foreach (var i in filter)
            {
                var entity1 = filter.GetEntity(i);
                ref var position1 = ref filter.Get1(i);
                var spriteRenderer1 = filter.Get2(i);
                var halfSize1 = spriteRenderer1.Value.bounds.size / 2;

                foreach (var j in filter)
                {
                    if (j <= i)
                    {
                        continue;
                    }

                    ref var position2 = ref filter.Get1(j);
                    var spriteRenderer2 = filter.Get2(j);
                    var halfSize2 = spriteRenderer2.Value.bounds.size / 2;

                    var delta = position1.Value - position2.Value;
                    var overlapX = (halfSize1.x + halfSize2.x) - Mathf.Abs(delta.x);
                    var overlapY = (halfSize1.y + halfSize2.y) - Mathf.Abs(delta.y);

                    if (overlapX <= 0 || overlapY <= 0)
                    {
                        continue;
                    }

                    Vector2 translationVector;

                    if (overlapX < overlapY) {
                        translationVector = new Vector2(delta.x < 0 ? -overlapX : overlapX, 0);
                    } else {
                        translationVector = new Vector2(0, delta.y < 0 ? -overlapY : overlapY);
                    }

                    if (player == entity1)
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
    }
}
