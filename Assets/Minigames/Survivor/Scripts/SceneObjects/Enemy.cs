using UnityEngine;

namespace Minigames.Survivor.Scripts.SceneObjects
{
    public class Enemy : MonoBehaviour
    {
        [field: SerializeField] public Transform Transform { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
    }
}
