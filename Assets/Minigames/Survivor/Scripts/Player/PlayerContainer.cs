using UnityEngine;

namespace Minigames.Survivor.Scripts.Player
{
    public class PlayerContainer : MonoBehaviour
    {
        [field: SerializeField] public Transform Transform { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
    }
}
