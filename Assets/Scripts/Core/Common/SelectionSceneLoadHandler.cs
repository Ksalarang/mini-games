using System;
using Core.StartScene;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using VContainer.Unity;

namespace Core.Common
{
    public class SelectionSceneLoadHandler : IInitializable, IDisposable
    {
        private readonly Button.ButtonClickedEvent onClick;
        private readonly SelectionSceneLoader selectionSceneLoader;

        public SelectionSceneLoadHandler(Button.ButtonClickedEvent onClick, SelectionSceneLoader selectionSceneLoader)
        {
            this.onClick = onClick;
            this.selectionSceneLoader = selectionSceneLoader;
        }

        void IInitializable.Initialize()
        {
            onClick.AddListener(OnClick);
        }

        void IDisposable.Dispose()
        {
            onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            onClick.RemoveListener(OnClick);
            selectionSceneLoader.LoadAsync().Forget();
        }
    }
}
