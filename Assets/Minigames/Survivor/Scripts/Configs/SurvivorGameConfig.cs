using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs
{
    [CreateAssetMenu(fileName = "SurvivorGameConfig", menuName = "Minigames/Survivor/GameConfig", order = 0)]
    public class SurvivorGameConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerConfig PlayerConfig { get; private set; }
        [field: SerializeField] public SpriteAnimationConfig SpriteAnimationConfig { get; private set; }
        [field: SerializeField] public EnemySpawnConfig EnemySpawnConfig { get; private set; }
        [field: SerializeField] public EnemyDamageConfig EnemyDamageConfig { get; private set; }
        [field: SerializeField] public WeaponConfig WeaponConfig { get; private set; }
        [field: SerializeField] public ExpItemConfig ExpItemConfig { get; private set; }
    }
}
