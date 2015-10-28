using System;
using System.Collections.Generic;
using System.Linq;
using CommonProvider.Factories;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents a set of Providers.
    /// </summary>
    public class SimpleProviders: ISimpleProviders
    {
        #region Fields

        readonly IEnumerable<Type> _providerTypes;
        readonly SimpleProviderFactoryBase _providerFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of SImple Providers with the specified provider 
        /// types and a provider factory.
        /// </summary>
        /// <param name="providerTypes">A collection of simple provider types.</param>
        /// <param name="providerFactory">The provider factory used to create simple providers 
        /// as requested.</param>
        public SimpleProviders(IEnumerable<Type> providerTypes,
            SimpleProviderFactoryBase providerFactory)
        {
            if (providerTypes == null || !providerTypes.Any())
            {
                throw new ArgumentException("providerTypes not set");
            }

            if (providerFactory == null)
            {
                throw new ArgumentNullException("providerFactory");
            }

            _providerTypes = providerTypes;
            _providerFactory = providerFactory;
        }

        /// <summary>
        /// Initializes a new instance of Simple Providers with the specified provider types. 
        /// It internally uses the default provider factory for creating providers.
        /// </summary>
        /// <param name="providerTypes">A collection of simple provider types.</param>
        public SimpleProviders(IEnumerable<Type> providerTypes)
            : this(providerTypes, new SimpleProviderFactory())
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the count of simple providers
        /// </summary>
        public int Count
        {
            get
            {
                return All<ISimpleProvider>().Count();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all simple providers of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the simple providers to get.</typeparam>
        /// <returns>The matching simple providers.</returns>
        public IEnumerable<T> All<T>() where T : ISimpleProvider
        {
            var providers =
                _providerTypes
                .Where(x =>
                    typeof(T).IsAssignableFrom(x))
                    .Select(x => _providerFactory.Create<T>(x));
                
            return providers;
        }

        /// <summary>
        /// Gets all simple providers.
        /// </summary>
        /// <returns>The simple providers.</returns>
        public IEnumerable<ISimpleProvider> All()
        {
            return All<ISimpleProvider>();
        }

        #endregion
    }
}
