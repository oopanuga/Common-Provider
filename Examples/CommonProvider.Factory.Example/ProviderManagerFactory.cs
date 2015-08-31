using CommonProvider.Configuration;
using CommonProvider.Factories;
using CommonProvider.ProviderLoaders;

namespace CommonProvider.Factory.Example
{
    public static class ProviderManagerFactory
    {
        public static IProviderManager Create()
        {
            var providerManager =
                new ProviderManager(
                    new ConfigProviderLoader(new ProviderConfigurationManager()),
                    new ProvidersFactory()
                    );

            return providerManager;
        }
    }
}
