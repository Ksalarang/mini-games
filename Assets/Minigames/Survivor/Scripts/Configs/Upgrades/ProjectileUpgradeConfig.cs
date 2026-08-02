using Minigames.Survivor.Scripts.Configs.Weapons;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Upgrades
{
    public abstract class ProjectileUpgradeConfig : UpgradeConfig
    {
        [field: SerializeField] public ProjectileType Type { get; private set; }
    }
}
