using System.Linq;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs.Weapons;
using Minigames.Survivor.Scripts.Ecs.Components.Weapons;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Upgrades
{
    public abstract class ProjectileUpgradeConfig : UpgradeConfig
    {
        [field: SerializeField] public ProjectileType Type { get; private set; }

        public override bool IsApplicableTo(EcsEntity player)
        {
            return player.Get<WeaponInventory>().Projectiles.Any(p => p.Type == Type);
        }
    }
}
