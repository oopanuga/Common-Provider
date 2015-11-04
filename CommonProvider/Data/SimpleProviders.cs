using CommonProvider.Factories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents a list of Simple Providers.
    /// </summary>
    public class SimpleProviders : SimpleProviders<ISimpleProvider>, ISimpleProviders
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
        public ISimpleProviders<T> All<T>() where T : ISimpleProvider
        {
            return new SimpleProviders<T>(_providerTypes, _providerFactory);
        }

        #endregion
    }

    /// <summary>
    /// Represents a generic list of Simple Providers.
    /// </summary>
    public class SimpleProviders<T> : ISimpleProviders<T>
        where T : ISimpleProvider
    {
        #region Fields

        protected readonly IEnumerable<Type> _providerTypes;
        protected readonly SimpleProviderFactoryBase _providerFactory;
        readonly IEnumerable<T> _providers;

        #endregion

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

        #region Methods

        /// <summary>
        /// Gets all simple providers.
        /// </summary>
        /// <returns>The list of simple providers.</returns>
        public ISimpleProviders<T> All()
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
                return All().Count();
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the Providers collection.
        /// </summary>
        /// <returns>A System.Collections.Generic.IEnumerator<T> that 
        /// can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (_providerTypes != null && _providerTypes.Any())
            {
                foreach (var _providerType in _providerTypes)
                {
                    if (typeof(T).IsAssignableFrom(_providerType))
                    {
                        yield return _providerFactory.Create<T>(_providerType);
                    }
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>A System.Collections.Generic.IEnumerator that 
        /// can be used to iterate through the collection.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
