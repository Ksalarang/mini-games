using UnityEngine;

namespace Core.Tools
{
    public class FrameRateSetter : MonoBehaviour
    {
        [SerializeField] private int targetFrameRate = 60;

        private void Start()
        {
            Application.targetFrameRate = targetFrameRate;
        }
    }
}