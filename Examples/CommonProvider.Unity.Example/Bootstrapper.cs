using CommonProvider.Configuration;
using CommonProvider.DependencyManagement;
using CommonProvider.Factories;
using CommonProvider.ProviderLoaders;
using Microsoft.Practices.Unity;

namespace CommonProvider.Unity.Example
{
    public static class Bootstrapper
    {
        static IUnityContainer _unityContainer;
        public static IUnityContainer UnityContainer
        {
            get
            {
                if (_unityContainer != null) return _unityContainer;

                _unityContainer = new UnityContainer();
                _unityContainer.RegisterType<IProviderConfigurationManager, ProviderConfigurationManager>();
                _unityContainer.RegisterType<ProviderLoaderBase, ConfigProviderLoader>();
                _unityContainer.RegisterType<IProvidersFactory, ProvidersFactory>(new InjectionConstructor());
                _unityContainer.RegisterType<IProviderManager, ProviderManager>();
                DependencyResolverService.SetResolver(new UnityDependencyResolver(_unityContainer));

                return _unityContainer;
            }
        }
    }
}
