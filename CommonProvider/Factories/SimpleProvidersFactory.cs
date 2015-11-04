using CommonProvider.Data;
using System;
using System.Collections.Generic;

namespace CommonProvider.Factories
{
    /// <summary>
    /// Represents the default implementation of ISimpleProvidersFactory. 
    /// It provides a means to create a set of Simple Providers.
    /// </summary>
    public class SimpleProvidersFactory : ISimpleProvidersFactory
    {
        readonly SimpleProviderFactoryBase _providerFactory;

        /// <summary>
        /// Initializes a new instance of SimpleProvidersFactory 
        /// with a specified provider factory.
        /// </summary>
        /// <param name="providerFactory">Creates a Simple Provider.</param>
        public SimpleProvidersFactory(SimpleProviderFactoryBase providerFactory)
        {
            if (providerFactory == null)
            {
                throw new ArgumentNullException("providerFactory");
            }

            _providerFactory = providerFactory;
        }

        /// <summary>
        /// The default constructor of SimpleProvidersFactory. The default provider 
        /// factory is used if the default constructor was called.
        /// </summary>
        public SimpleProvidersFactory() { }

        /// <summary>
        /// Creates Simple Providers based on the specified Provider Types.
        /// </summary>
        /// <param name="providerTypes">The providers types.</param>
        /// <returns>The created Simple Providers.</returns>
        public ISimpleProviders Create(IEnumerable<Type> providerTypes)
        {
            if (_providerFactory == null)
            {
                return new SimpleProviders(providerTypes);
            }
            else
            {
                return new SimpleProviders(providerTypes,
                    _providerFactory);
            }
        }
    }
}
