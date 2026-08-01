using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Survivor.Scripts.UI
{
    public class UpgradeCardView : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text Title { get; private set; }
        [field: SerializeField] public TMP_Text Description { get; private set; }
        [field: SerializeField] public Button Button { get; private set; }
    }
}
