using CommonProvider.Configuration;
using CommonProvider.DependencyManagement;
using CommonProvider.DependencyResolvers;
using CommonProvider.Factories;
using CommonProvider.ProviderLoaders;
using Microsoft.Practices.Unity;

namespace CommonProvider.Example
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
                _unityContainer.RegisterType<ProviderLoaderBase, ConfigProviderLoader>(new InjectionConstructor(
                    _unityContainer.Resolve<IProviderConfigurationManager>()));
                _unityContainer.RegisterType<IProvidersFactory, ProvidersFactory>(new InjectionConstructor());
                _unityContainer.RegisterType<IProviderManager, ProviderManager>(new InjectionConstructor(
                    _unityContainer.Resolve<ProviderLoaderBase>(), _unityContainer.Resolve<IProvidersFactory>()));
                DependencyResolverService.SetResolver(new UnityDependencyResolver(_unityContainer));

                return _unityContainer;
            }
        }
    }
}
