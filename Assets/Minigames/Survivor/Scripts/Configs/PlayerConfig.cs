using UnityEngine;

namespace Minigames.Survivor.Scripts.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Minigames/Survivor/PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
    }
}
