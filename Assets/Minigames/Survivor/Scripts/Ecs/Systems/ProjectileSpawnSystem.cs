using System;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Events;
using Minigames.Survivor.Scripts.Ecs.Components.Player;
using Minigames.Survivor.Scripts.Ecs.Components.Requests;
using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class ProjectileSpawnSystem : IEcsRunSystem
    {
        private readonly WeaponConfig config;
        private readonly SurvivorSceneContainer sceneContainer;

        private readonly EcsWorld world;
        private readonly EcsFilter<TimerComponent, ProjectileSpawnRequest, TimerExpiredEvent> filter;
        private readonly EcsFilter<PlayerTag> playerFilter;

        public ProjectileSpawnSystem(WeaponConfig config, SurvivorSceneContainer sceneContainer)
        {
            this.config = config;
            this.sceneContainer = sceneContainer;
        }

        public void Run()
        {
            foreach (var i in filter)
            {
                Spawn(filter.Get2(i).Data);
            }
        }

        private void Spawn(ProjectileData data)
        {
            var projectile = Object.Instantiate(config.WeaponPrefab, sceneContainer.World);

            var entity = world.NewEntity();
            entity.Get<TransformComponent>().Value = projectile.transform;

            ref var spriteRendererComponent = ref entity.Get<SpriteRendererComponent>();
            spriteRendererComponent.Value = projectile.GetComponent<SpriteRenderer>();
            spriteRendererComponent.Value.sprite = data.Sprite;

            var player = playerFilter.GetEntity(0);
            entity.Get<Position>().Value = player.Get<Position>().Value;
            entity.Get<BoundsComponent>().HalfSize = spriteRendererComponent.Value.bounds.size * 0.5f;
            entity.Get<Speed>().Value = data.Speed;
            entity.Get<DamageComponent>().Value = data.Damage;

            switch (data.DirectionType)
            {
                case ProjectileDirectionType.Player:
                    var playerDirection = player.Get<DirectionComponent>();
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

                    entity.Get<DirectionComponent>().Value = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            entity.Get<RotationComponent>().RotateTowardsDirection = true;
        }
    }
}
