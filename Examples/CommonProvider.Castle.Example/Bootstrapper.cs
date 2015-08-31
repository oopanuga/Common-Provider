using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommonProvider.Configuration;
using CommonProvider.Data.Parsers;
using CommonProvider.DependencyManagement;
using CommonProvider.Example.Shared.Providers;
using CommonProvider.Factories;
using CommonProvider.ProviderLoaders;

namespace CommonProvider.Castle.Example
{
    public static class Bootstrapper
    {
        static IWindsorContainer _castleContainer;
        public static IWindsorContainer CastleContainer
        {
            get
            {
                if (_castleContainer != null) return _castleContainer;

                _castleContainer = new WindsorContainer();
                _castleContainer.Register(Component.For<IProviderConfigurationManager>().ImplementedBy<ProviderConfigurationManager>());
                _castleContainer.Register(Component.For<ProviderLoaderBase>().ImplementedBy<ConfigProviderLoader>());
                _castleContainer.Register(Component.For<IProvidersFactory>().ImplementedBy<ProvidersFactory>());
                _castleContainer.Register(Component.For<IProviderManager>().ImplementedBy<ProviderManager>());
                _castleContainer.Register(AllTypes.FromAssembly(typeof(IDataParser).Assembly).BasedOn<PipedDataParser>());
                _castleContainer.Register(AllTypes.FromAssembly(typeof(ISmsProvider).Assembly).BasedOn<ISmsProvider>());
                
                DependencyResolverService.SetResolver(new CastleDependencyResolver(_castleContainer));

                return _castleContainer;
            }
        }
    }
}
