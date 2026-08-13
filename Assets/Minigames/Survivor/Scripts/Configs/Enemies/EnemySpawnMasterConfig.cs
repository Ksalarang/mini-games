using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Enemies
{
    [CreateAssetMenu(fileName = "EnemySpawnMasterConfig", menuName = "Minigames/Survivor/Enemies/EnemySpawnMasterConfig", order = 0)]
    public class EnemySpawnMasterConfig : ScriptableObject
    {
        [field: SerializeField] public SmallEnemySpawnConfig[] SmallEnemyConfigs { get; private set; }
        [field: SerializeField] public MediumEnemySpawnConfig[] MediumEnemyConfigs { get; private set; }
        [field: SerializeField] public EnemySpawnConfig[] BigEnemyConfigs { get; private set; }

        [field: Space, SerializeField] public GameObject EnemyPrefab { get; private set; }
    }
}
