using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components.Weapons;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Upgrades
{
    [CreateAssetMenu(fileName = "ProjectileDamageUpgradeConfig", menuName = "Minigames/Survivor/Upgrades/ProjectileDamageUpgradeConfig", order = 0)]
    public class ProjectileDamageUpgradeConfig : ProjectileUpgradeConfig
    {
        public override void Apply(EcsEntity player)
        {
            var projectiles = player.Get<WeaponInventory>().Projectiles;

            for (var i = 0; i < projectiles.Count; i++)
            {
                var projectile = projectiles[i];

                if (projectile.Type == Type)
                {
                    projectile.Damage *= 1 + NormalizedValue;
                    projectiles[i] = projectile;
                    return;
                }
            }
        }
    }
}
