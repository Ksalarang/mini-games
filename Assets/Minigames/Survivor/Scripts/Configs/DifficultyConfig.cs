using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs
{
    [CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Minigames/Survivor/DifficultyConfig", order = 0)]
    public class DifficultyConfig : ScriptableObject
    {
        [field: SerializeField] public float InitialEnemySpanRate { get; private set; }
        [field: SerializeField] public int MaxEnemyCount { get; private set; }
        [field: SerializeField] public int SessionDurationMinutes { get; private set; }

        public float SessionDurationSeconds => SessionDurationMinutes * 60f;
    }
}
