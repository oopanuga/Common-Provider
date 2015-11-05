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

        protected readonly IEnumerable<Type> _providerTypes;
        protected readonly SimpleProviderFactoryBase _providerFactory;

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

            this._providerTypes = providerTypes;
            this._providerFactory = providerFactory;
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
        /// Returns an enumerator that iterates through the Providers collection.
        /// </summary>
        /// <returns>A System.Collections.Generic.IEnumerator<T> that 
        /// can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (this._providerTypes != null && this._providerTypes.Any())
            {
                foreach (var _providerType in this._providerTypes)
                {
                    if (typeof(T).IsAssignableFrom(_providerType))
                    {
                        yield return this._providerFactory.Create<T>(_providerType);
                    }
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>A System.Collections.Generic.IEnumerator that 
        /// can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}