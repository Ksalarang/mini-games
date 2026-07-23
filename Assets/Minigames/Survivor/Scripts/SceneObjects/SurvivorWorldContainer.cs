using UnityEngine;

namespace Minigames.Survivor.Scripts.SceneObjects
{
    public class SurvivorWorldContainer : MonoBehaviour
    {
        [field: SerializeField] public Transform Enemies { get; private set; }
        [field: SerializeField] public Transform Projectiles { get; private set; }
    }
}
