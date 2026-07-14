using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs
{
    [CreateAssetMenu(fileName = "PlayerMoveConfig", menuName = "Minigames/Survivor/PlayerMoveConfig", order = 0)]
    public class PlayerMoveConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
    }
}
