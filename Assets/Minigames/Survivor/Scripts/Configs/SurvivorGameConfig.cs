using Minigames.Survivor.Scripts.Configs.Enemies;
using Minigames.Survivor.Scripts.Configs.Upgrades;
using Minigames.Survivor.Scripts.Configs.Weapons;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs
{
    [CreateAssetMenu(fileName = "SurvivorGameConfig", menuName = "Minigames/Survivor/GameConfig", order = 0)]
    public class SurvivorGameConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerConfig PlayerConfig { get; private set; }
        [field: SerializeField] public SpriteAnimationConfig SpriteAnimationConfig { get; private set; }
        [field: SerializeField] public EnemySpawnMasterConfig EnemySpawnMasterConfig { get; private set; }
        [field: SerializeField] public EnemyDamageConfig EnemyDamageConfig { get; private set; }
        [field: SerializeField] public WeaponBundleConfig WeaponBundleConfig { get; private set; }
        [field: SerializeField] public ExpItemConfig ExpItemConfig { get; private set; }
        [field: SerializeField] public UpgradeBundleConfig UpgradeBundleConfig { get; private set; }
        [field: SerializeField] public DifficultyConfig DifficultyConfig { get; private set; }
    }
}
