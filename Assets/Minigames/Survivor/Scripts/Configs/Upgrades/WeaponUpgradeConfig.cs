using System.Linq;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs.Weapons;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.Ecs.Components.Weapons;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Upgrades
{
    public abstract class WeaponUpgradeConfig : UpgradeConfig
    {
        [field: SerializeField] public WeaponId WeaponId { get; private set; }

        public override bool IsApplicableTo(EcsEntity player)
        {
            return player.Get<WeaponInventory>().Weapons.Any(w => w.Get<WeaponComponent>().Id == WeaponId);
        }
    }
}
