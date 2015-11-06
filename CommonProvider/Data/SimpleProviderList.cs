using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using CommonProvider.Factories;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents a generic list of Simple Providers.
    /// </summary>
    /// <typeparam name="T">The type of Simple Provider.</typeparam>
    public class SimpleProviderList<T> : ISimpleProviderList<T>
        where T : ISimpleProvider
    {
        #region Fields

        /// <summary>
        /// Gets a list of Provider Types
        /// </summary>
        protected readonly IEnumerable<Type> ProviderTypes;

        /// <summary>
        /// Gets a Simple Provider Factory
        /// </summary>
        protected readonly SimpleProviderFactoryBase ProviderFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Simple Providers with the specified provider 
        /// types and a provider factory.
        /// </summary>
        /// <param name="providerTypes">A collection of simple provider types.</param>
        /// <param name="providerFactory">The provider factory used to create simple providers 
        /// as requested.</param>
        public SimpleProviderList(IEnumerable<Type> providerTypes, SimpleProviderFactoryBase providerFactory)
        {
            if (providerTypes == null || !providerTypes.Any())
            {
                throw new ArgumentException("providerTypes not set");
            }

            if (providerFactory == null)
            {
                throw new ArgumentNullException("providerFactory");
            }

            this.ProviderTypes = providerTypes;
            this.ProviderFactory = providerFactory;
        }

        /// <summary>
        /// Initializes a new instance of Simple Providers with the specified provider types. 
        /// It internally uses the default provider factory for creating providers.
        /// </summary>
        /// <param name="providerTypes">A collection of simple provider types.</param>
        public SimpleProviderList(IEnumerable<Type> providerTypes)
            : this(providerTypes, new SimpleProviderFactory())
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all simple providers.
        /// </summary>
        /// <returns>The list of simple providers.</returns>
        public ISimpleProviderList<T> All()
        {
            return this;
        }

        /// <summary>
        /// Gets the count of providers.
        /// </summary>
        public int Count
        {
            get
            {
                return this.All().Count();
            }
        }

        /// <summary>
        /// Returns a generic enumerator that iterates through the Simple Provider List.
        /// </summary>
        /// <returns>The generic enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (this.ProviderTypes != null && this.ProviderTypes.Any())
            {
                foreach (var _providerType in this.ProviderTypes)
                {
                    if (typeof(T).IsAssignableFrom(_providerType))
                    {
                        yield return this.ProviderFactory.Create<T>(_providerType);
                    }
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the Simple Provider List.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}