using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Tools;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class TimerSystem : IEcsRunSystem
    {
        private readonly GameTimeService timeService; //todo: use everywhere

        private readonly EcsFilter<Timer> filter;

        public TimerSystem(GameTimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var timer = ref filter.Get1(i);
                ref var entity = ref filter.GetEntity(i);

                if (entity.Has<TimerExpired>())
                {
                    entity.Del<TimerExpired>();
                }

                timer.TimeLeft -= timeService.DeltaTime;

                if (timer.TimeLeft > 0f)
                {
                    continue;
                }

                entity.Get<TimerExpired>();

                if (timer.Interval > 0f)
                {
                    timer.TimeLeft += timer.Interval;
                }
                else
                {
                    entity.Del<Timer>();
                }
            }
        }
    }
}
