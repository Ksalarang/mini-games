using Core.Scenes;
using UnityEngine;

namespace Core.MinigameSelectionScene
{
    [CreateAssetMenu(fileName = "MinigameConfig", menuName = "MinigameSelection/vb ghMinigameConfig", order = 0)]
    public class MinigameConfig : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }

        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public SceneParams SceneParams { get; private set; }
    }
}
