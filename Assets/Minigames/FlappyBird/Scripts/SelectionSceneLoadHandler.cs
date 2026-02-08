using System;
using Core.StartScene;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Minigames.FlappyBird.Scripts
{
    public class SelectionSceneLoadHandler : IInitializable, IDisposable
    {
        private readonly SceneContainer sceneContainer;
        private readonly SelectionSceneLoader selectionSceneLoader;

        public SelectionSceneLoadHandler(SceneContainer sceneContainer, SelectionSceneLoader selectionSceneLoader)
        {
            this.sceneContainer = sceneContainer;
            this.selectionSceneLoader = selectionSceneLoader;
        }

        void IInitializable.Initialize()
        {
            sceneContainer.BackButton.onClick.AddListener(LoadSelectionScene);
        }

        void IDisposable.Dispose()
        {
            sceneContainer.BackButton.onClick.RemoveListener(LoadSelectionScene);
        }

        private void LoadSelectionScene()
        {
            sceneContainer.BackButton.onClick.RemoveListener(LoadSelectionScene);
            selectionSceneLoader.LoadAsync().Forget();
        }
    }
}