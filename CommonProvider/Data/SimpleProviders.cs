using CommonProvider.Factories;
using System;
using System.Collections.Generic;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents a list of Simple Providers.
    /// </summary>
    public class SimpleProviders : SimpleProviderList<ISimpleProvider>, ISimpleProviders
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of Simple Providers with the specified provider 
        /// types and a provider factory.
        /// </summary>
        /// <param name="providerTypes">A collection of simple provider types.</param>
        /// <param name="providerFactory">The provider factory used to create simple providers 
        /// as requested.</param>
        public SimpleProviders(IEnumerable<Type> providerTypes,
            SimpleProviderFactoryBase providerFactory)
            : base(providerTypes, providerFactory)
        {
        }

        /// <summary>
        /// Initializes a new instance of Simple Providers with the specified provider types. 
        /// It internally uses the default provider factory for creating providers.
        /// </summary>
        /// <param name="providerTypes">A collection of simple provider types.</param>
        public SimpleProviders(IEnumerable<Type> providerTypes)
            : base(providerTypes)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all simple providers of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the simple providers.</typeparam>
        /// <returns>The matching simple providers.</returns>
        public ISimpleProviderList<T> All<T>() where T : ISimpleProvider
        {
            return new SimpleProviderList<T>(_providerTypes, _providerFactory);
        }

        #endregion
    }
}
