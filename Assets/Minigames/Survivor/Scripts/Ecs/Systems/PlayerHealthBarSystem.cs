using Core.Tools.Extensions;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Player;
using Minigames.Survivor.Scripts.SceneObjects;
using Minigames.Survivor.Scripts.UI;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class PlayerHealthBarSystem : IEcsRunSystem
    {
        private readonly ProgressBarView healthBar;

        private readonly EcsFilter<PlayerTag, Health> filter;

        private float prevHealth;

        public PlayerHealthBarSystem(PlayerContainer playerContainer)
        {
            healthBar = playerContainer.HealthBar;
        }

        public void Run()
        {
            var health = filter.Get2(0);

            if (Mathf.Approximately(health.Value, prevHealth))
            {
                return;
            }

            prevHealth = health.Value;

            var width = health.Value / health.MaxValue * healthBar.MaxFillWidth;
            healthBar.FillTransform.SetLocalScaleX(width);
            healthBar.FillTransform.localPosition = Vector3.zero;

            var xOffset = (healthBar.BackGroundTransform.localScale.x - healthBar.FillTransform.localScale.x) * 0.5f;
            var newPosition = healthBar.FillTransform.localPosition;
            newPosition.x -= xOffset;
            healthBar.FillTransform.localPosition = newPosition;
        }
    }
}
