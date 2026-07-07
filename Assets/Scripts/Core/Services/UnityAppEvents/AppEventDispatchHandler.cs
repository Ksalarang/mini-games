using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Core.Services.UnityAppEvents
{
    public class AppEventDispatchHandler : IInitializable, IDisposable
    {
        private readonly AppEventDispatcher appEventDispatcher;
        private readonly IReadOnlyList<IAppPauseListener> appPauseListeners;

        public AppEventDispatchHandler(AppEventDispatcher appEventDispatcher,
            IReadOnlyList<IAppPauseListener> appPauseListeners)
        {
            this.appEventDispatcher = appEventDispatcher;
            this.appPauseListeners = appPauseListeners;
        }

        void IInitializable.Initialize()
        {
            appEventDispatcher.OnAppPause += OnAppPause;
        }

        void IDisposable.Dispose()
        {
            appEventDispatcher.OnAppPause -= OnAppPause;
        }

        private void OnAppPause(bool paused)
        {
            foreach (var listener in appPauseListeners)
            {
                listener.OnAppPause(paused);
            }
        }
    }
}
