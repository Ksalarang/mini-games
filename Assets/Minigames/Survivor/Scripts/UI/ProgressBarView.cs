using UnityEngine;

namespace Minigames.Survivor.Scripts.UI
{
    public class ProgressBarView : MonoBehaviour
    {
        [field: SerializeField] public Transform BackGroundTransform { get; private set; }
        [field: SerializeField] public Transform FillTransform { get; private set; }

        public float MaxFillWidth { get; private set; }

        private void Awake()
        {
            MaxFillWidth = FillTransform.localScale.x;
        }
    }
}
