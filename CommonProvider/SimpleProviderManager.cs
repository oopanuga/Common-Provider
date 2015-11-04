using CommonProvider.Data;
using CommonProvider.Factories;
using CommonProvider.ProviderLoaders;
using System;

namespace CommonProvider
{
    /// <summary>
    /// Represents the default implementation of ISimpleProviderManager. 
    /// This is the gateway to all simple providers
    /// </summary>
    public sealed class SimpleProviderManager : ISimpleProviderManager
    {
        #region Constructors

        /// <summary>
        /// Initializes an instance of SimpleProviderManager using the specified 
        /// provider loader and provider list factory.
        /// </summary>
        /// <param name="providerLoader">The provider loader to use in loading 
        /// the providers.</param>
        /// <param name="providersFactory">The Providers factory to use 
        /// in creating the set of providers.</param>
        public SimpleProviderManager(SimpleProviderLoaderBase providerLoader,
            ISimpleProvidersFactory providersFactory)
        {
            if (providerLoader == null)
            {
                throw new ArgumentNullException("providerLoader");
            }

            if (providersFactory == null)
            {
                throw new ArgumentNullException("providersFactory");
            }

            var providerTypes = providerLoader.Load();
            Providers = providersFactory.Create(providerTypes);
        }

        /// <summary>
        /// Initializes an instance of SimpleProviderManager using the specified provider loader. 
        /// It internally uses the default providers factory to create the provider list.
        /// </summary>
        /// <param name="providerLoader">The provider loader to use in loading the providers.</param>
        public SimpleProviderManager(SimpleProviderLoaderBase providerLoader)
            : this(providerLoader, new SimpleProvidersFactory())
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the set of loaded providers.
        /// </summary>
        public ISimpleProviders Providers { get; private set; }

        #endregion
    }
}
