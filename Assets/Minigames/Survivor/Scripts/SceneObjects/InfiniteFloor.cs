using UnityEngine;

namespace Minigames.Survivor.Scripts.SceneObjects
{
    public class InfiniteFloor : MonoBehaviour
    {
        [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }

        [field: SerializeField] public float TextureWorldSize { get; private set; }
        [field: SerializeField] public float Margin { get; private set; }
    }
}
