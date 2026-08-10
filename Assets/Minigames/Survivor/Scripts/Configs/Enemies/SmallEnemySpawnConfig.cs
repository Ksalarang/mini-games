using Core.Tools.Structs;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Enemies
{
    [CreateAssetMenu(fileName = "SmallEnemySpawnConfig", menuName = "Minigames/Survivor/Enemies/SmallEnemySpawnConfig")]
    public class SmallEnemySpawnConfig : EnemySpawnConfig
    {
        [field: SerializeField] public IntRange ReplacePeriodMinutes { get; private set; }

        public IntRange ReplacePeriodSeconds => new()
            {
                Min = ReplacePeriodMinutes.Min * 60,
                Max = ReplacePeriodMinutes.Max * 60
            };
    }
}
