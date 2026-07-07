using System;
using UnityEngine;

namespace Core.Services.UnityAppEvents
{
    public class AppEventDispatcher : MonoBehaviour
    {
        public event Action<bool> OnAppPause;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            OnAppPause?.Invoke(pauseStatus);
        }
    }
}
