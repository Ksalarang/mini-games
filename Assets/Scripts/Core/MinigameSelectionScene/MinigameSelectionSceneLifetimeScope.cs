using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.MinigameSelectionScene
{
    public class MinigameSelectionSceneLifetimeScope : LifetimeScope
    {
        [SerializeField] private MinigameSelectionView minigameSelectionView;
        [SerializeField] private MinigameSelectionConfig minigameSelectionConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(minigameSelectionView);
            
            builder.RegisterComponent(minigameSelectionConfig);

            builder.RegisterEntryPoint<MinigameSelectionPresenter>();
        }
    }
}