using System.Linq;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Ecs.Components;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class AnimationSpriteSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly SpriteAnimationConfig config;

        private EcsFilter<SpriteAnimation, MoveStateComponent> filter;

        public AnimationSpriteSystem(SpriteAnimationConfig config)
        {
            this.config = config;
        }

        public void Init()
        {
            foreach (var i in filter)
            {
                ref var animation = ref filter.Get1(i);
                var moveState = filter.Get2(i);
                var data = config.Data.First(d => d.State == moveState.CurrentValue);

                animation.Sprites = data.Sprites;
                animation.FramesPerSecond = data.FramesPerSecond;
            }
        }

        public void Run()
        {
            foreach (var i in filter)
            {
                var moveState = filter.Get2(i);

                if (moveState.CurrentValue == moveState.PreviousValue)
                {
                    continue;
                }

                var data = config.Data.First(d => d.State == moveState.CurrentValue);
                ref var animation = ref filter.Get1(i);
                animation.Sprites = data.Sprites;
                animation.FramesPerSecond = data.FramesPerSecond;
                animation.CurrentIndex = 0;
                animation.CurrentTimeSeconds = 0f;
            }
        }
    }
}
