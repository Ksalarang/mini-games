using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Tools;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class VelocitySystem : IEcsRunSystem
    {
        private readonly GameTimeService timeService;

        private readonly EcsFilter<DirectionComponent, Speed> filter;

        public VelocitySystem(GameTimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run()
        {
            foreach (var i in filter)
            {
                var direction = filter.Get1(i);
                var speed = filter.Get2(i);
                ref var velocity = ref filter.GetEntity(i).Get<Velocity>();

                var value = direction.Value * (timeService.DeltaTime * speed.Value);
                velocity.Value = value;
            }
        }
    }
}
