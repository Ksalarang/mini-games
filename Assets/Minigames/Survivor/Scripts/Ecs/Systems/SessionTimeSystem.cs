using System;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class SessionTimeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld world;
        private readonly EcsFilter<SessionTimeComponent> sessionTimeFilter;

        private DateTime startTime;

        public void Init()
        {
            startTime = DateTime.Now;
            world.NewEntity().Get<SessionTimeComponent>();
        }

        public void Run()
        {
            sessionTimeFilter.Get1(0).Value = DateTime.Now - startTime;
        }
    }
}
