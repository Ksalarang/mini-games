using Minigames.Survivor.Scripts.SceneObjects;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ExpItemConfig", menuName = "Minigames/Survivor/ExpItemConfig", order = 0)]
    public class ExpItemConfig : ScriptableObject
    {
        [field: SerializeField] public SpriteObject Prefab { get; private set; }
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float SpawnChance { get; private set; }
    }
}
