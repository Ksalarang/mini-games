using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.MinigameSelectionScene
{
    public class MinigameButtonView : MonoBehaviour
    {
        [field: SerializeField] public Button Button { get; private set; }

        [field: SerializeField] public TMP_Text Label { get; private set; }
    }
}