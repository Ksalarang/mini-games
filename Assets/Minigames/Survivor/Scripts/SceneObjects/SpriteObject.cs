using UnityEngine;

namespace Minigames.Survivor.Scripts.SceneObjects
{
    public class SpriteObject : MonoBehaviour
    {
        [field: SerializeField] public Transform Transform { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
    }
}
