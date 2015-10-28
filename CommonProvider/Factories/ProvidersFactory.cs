using CommonProvider.Data;
using System;
using System.Collections.Generic;

namespace CommonProvider.Factories
{
    /// <summary>
    /// Represents the default implementation of IProvidersFactory. 
    /// It provides a means to create a set of Providers.
    /// </summary>
    public class ProvidersFactory : IProvidersFactory
    {
        readonly ProviderFactoryBase _providerFactory;

        /// <summary>
        /// Initializes a new instance of ProvidersFactory 
        /// with a specified provider factory.
        /// </summary>
        /// <param name="providerFactory">Creates a Provider.</param>
        public ProvidersFactory(ProviderFactoryBase providerFactory)
        {
            if (providerFactory == null)
            {
                throw new ArgumentNullException("providerFactory");
            }

            _providerFactory = providerFactory;
        }

        /// <summary>
        /// The default constructor of ProvidersFactory. The default provider 
        /// factory is used if the default constructor was called.
        /// </summary>
        public ProvidersFactory() { }

        /// <summary>
        /// Creates a Provider List based on the specified Provider Descriptors.
        /// </summary>
        /// <param name="providerDescriptors">Holds information regarding the providers.</param>
        /// <returns>The created Providers.</returns>
        public IProviders Create(IEnumerable<IProviderDescriptor> providerDescriptors)
        {
            if (_providerFactory == null)
            {
                return new Providers(providerDescriptors);
            }
            else
            {
                return new Providers(providerDescriptors, 
                    _providerFactory);
            }
        }
    }
}
