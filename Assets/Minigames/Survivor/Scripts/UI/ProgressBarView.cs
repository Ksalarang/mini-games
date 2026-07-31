using UnityEngine;

namespace Minigames.Survivor.Scripts.UI
{
    public class ProgressBarView : MonoBehaviour
    {
        [SerializeField] private RectTransform fill;

        public void SetProgress(float value)
        {
            var anchorMax = fill.anchorMax;
            anchorMax.x = Mathf.Clamp01(value);
            fill.anchorMax = anchorMax;
        }
    }
}
