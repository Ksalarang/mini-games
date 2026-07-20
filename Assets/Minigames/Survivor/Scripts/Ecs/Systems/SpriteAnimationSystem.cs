using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class SpriteAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SpriteRendererComponent, SpriteAnimationComponent> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var animation = ref filter.Get2(i);

                if (animation.CurrentTimeSeconds <= 0f)
                {
                    animation.CurrentTimeSeconds += 1f / animation.FramesPerSecond;

                    ref var renderer = ref filter.Get1(i);
                    var index = animation.CurrentIndex++ % animation.Sprites.Length;
                    renderer.Value.sprite = animation.Sprites[index];
                }

                animation.CurrentTimeSeconds -= Time.deltaTime;
            }
        }
    }
}
