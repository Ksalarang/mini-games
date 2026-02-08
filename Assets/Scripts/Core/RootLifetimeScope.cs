using Core.Services;
using Core.StartScene;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<AddressablesSceneService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AddressablesAssetService>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.RegisterEntryPoint<SelectionSceneLoader>().AsSelf();
        }
    }
}