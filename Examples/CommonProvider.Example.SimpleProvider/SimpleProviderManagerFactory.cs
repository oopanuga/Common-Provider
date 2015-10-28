using CommonProvider.ProviderLoaders;
using System;

namespace CommonProvider.Example.SimpleProvider
{
    public static class SimpleProviderManagerFactory
    {
        public static ISimpleProviderManager Create()
        {
            var providerManager =
                new SimpleProviderManager(
                    new DirectoryProviderLoader(Environment.CurrentDirectory)
                    );

            return providerManager;
        }
    }
}
