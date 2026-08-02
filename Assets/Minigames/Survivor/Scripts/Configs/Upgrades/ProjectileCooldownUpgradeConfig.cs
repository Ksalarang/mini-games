using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs.Weapons;
using Minigames.Survivor.Scripts.Ecs.Components.Weapons;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Upgrades
{
    [CreateAssetMenu(fileName = "ProjectileCooldownUpgradeConfig", menuName = "Minigames/Survivor/Upgrades/ProjectileCooldownUpgradeConfig", order = 0)]
    public class ProjectileCooldownUpgradeConfig : UpgradeConfig
    {
        [field: SerializeField] public ProjectileType Type { get; private set; }
        [field: SerializeField] public float MinCooldown { get; private set; } = 0.1f;

        public override void Apply(EcsEntity player)
        {
            var projectiles = player.Get<WeaponInventory>().Projectiles;

            for (var i = 0; i < projectiles.Count; i++)
            {
                var projectile = projectiles[i];

                if (projectile.Type == Type)
                {
                    projectile.Cooldown = Mathf.Max(MinCooldown, projectile.Cooldown * (1 - NormalizedValue));
                    projectiles[i] = projectile;
                    return;
                }
            }
        }
    }
}
