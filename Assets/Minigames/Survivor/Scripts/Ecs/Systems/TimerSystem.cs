using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.Tools;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class TimerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly GameTimeService timeService;

        private readonly EcsWorld world;
        private readonly EcsFilter<TimerComponent> filter;

        public TimerSystem(GameTimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Init()
        {
            var entity = world.NewEntity();
            entity.Get<OneSecondTimerEvent>();
            ref var timer = ref entity.Get<TimerComponent>();
            timer.TimeLeft = timer.Interval = 1f;
        }

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var timer = ref filter.Get1(i);
                ref var entity = ref filter.GetEntity(i);

                timer.TimeLeft -= timeService.DeltaTime;

                if (timer.TimeLeft > 0f)
                {
                    continue;
                }

                entity.Get<TimerExpiredEvent>();

                if (timer.Interval > 0f)
                {
                    timer.TimeLeft += timer.Interval;
                }
                else
                {
                    entity.Del<TimerComponent>();
                }
            }
        }
    }
}
