using Minigames.Survivor.Scripts.Ecs.Components;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs.Enemies
{
    public abstract class EnemySpawnConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyType EnemyType { get; private set; }
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
        [field: SerializeField] public int FramesPerSecond { get; private set; } = 4;
        [field: SerializeField] public float MoveSpeed { get; private set; } = 2f;
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public int MinPlayerLevel { get; private set; }
    }
}
