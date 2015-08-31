using Autofac;
using CommonProvider.Configuration;
using CommonProvider.Data.Parsers;
using CommonProvider.DependencyManagement;
using CommonProvider.Example.Shared.Providers;
using CommonProvider.Factories;
using CommonProvider.ProviderLoaders;

namespace CommonProvider.Autofac.Example
{
    public static class Bootstrapper
    {
        static IContainer _autofacContainer;
        public static IContainer AutofacContainer
        {
            get
            {
                if (_autofacContainer != null) return _autofacContainer;

                var _containerBuilder = new ContainerBuilder();

                _containerBuilder.RegisterType<ProviderManager>().As<IProviderManager>();
                _containerBuilder.RegisterType<ProvidersFactory>().As<IProvidersFactory>();
                _containerBuilder.RegisterType<ConfigProviderLoader>().As<ProviderLoaderBase>();
                _containerBuilder.RegisterType<ProviderConfigurationManager>().As<IProviderConfigurationManager>();
                _containerBuilder.Register(c => new PipedDataParser());
                _containerBuilder.RegisterAssemblyTypes(typeof(ISmsProvider).Assembly).AssignableTo<ISmsProvider>();
                _autofacContainer = _containerBuilder.Build();

                DependencyResolverService.SetResolver(new AutofacDependencyResolver(_autofacContainer));

                return _autofacContainer;
            }
        }
    }
}
