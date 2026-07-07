using Core.Services;
using Core.Services.Storage;
using Core.Services.UnityAppEvents;
using Core.StartScene;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class RootLifetimeScope : LifetimeScope
    {
        [Space]
        [SerializeField] private AppEventDispatcher appEventDispatcher;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(appEventDispatcher);

            builder.Register<AddressablesSceneService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AddressablesAssetService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerPrefsStorage>(Lifetime.Singleton).As<IStorage>();

            builder.RegisterEntryPoint<SelectionSceneLoader>().AsSelf();
        }
    }
}