using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs
{
    [CreateAssetMenu(fileName = "MoveConfig", menuName = "Minigames/Survivor/MoveConfig", order = 0)]
    public class MoveConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
    }
}
