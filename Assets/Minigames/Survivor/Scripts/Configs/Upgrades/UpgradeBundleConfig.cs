using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Upgrades
{
    [CreateAssetMenu(fileName = "UpgradeBundleConfig", menuName = "Minigames/Survivor/Upgrades/UpgradeBundleConfig", order = 0)]
    public class UpgradeBundleConfig : ScriptableObject
    {
        [field: SerializeField] public UpgradeConfig[] Upgrades { get; private set; }

        [field: Space, SerializeField] public int FirstLevelExp { get; private set; }
    }
}
