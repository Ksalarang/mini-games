using System;
using System.Linq;
using Minigames.Survivor.Scripts.Common;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Enemies
{
    [CreateAssetMenu(fileName = "EnemySpawnMasterConfig", menuName = "Minigames/Survivor/Enemies/EnemySpawnMasterConfig")]
    public class EnemySpawnMasterConfig : ScriptableObject
    {
        [field: SerializeField] public SmallEnemySpawnConfig[] SmallEnemyConfigs { get; private set; }
        [field: SerializeField] public MediumEnemySpawnConfig[] MediumEnemyConfigs { get; private set; }
        [field: SerializeField] public EnemySpawnConfig[] LargeEnemyConfigs { get; private set; }

        public EnemySpawnConfig GetConfig(EnemyType type, EnemyId id)
        {
            return type switch
            {
                EnemyType.Small => SmallEnemyConfigs.First(c => c.EnemyId == id),
                EnemyType.Medium => MediumEnemyConfigs.First(c => c.EnemyId == id),
                EnemyType.Large => LargeEnemyConfigs.First(c => c.EnemyId == id),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
