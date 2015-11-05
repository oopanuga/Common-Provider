using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using CommonProvider.Factories;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents a generic list of Providers.
    /// </summary>
    /// <typeparam name="T">The type of Provider.</typeparam>
    public class ProviderList<T> : IProviderList<T>
        where T : IProvider
    {
        #region Fields

        protected readonly IEnumerable<IProviderDescriptor> _providerDescriptors;
        protected readonly ProviderFactoryBase _providerFactory;
        protected readonly IEnumerable<T> _providers;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Providers with the specified provider 
        /// descriptors and a provider factory.
        /// </summary>
        /// <param name="providerDescriptors">A collection of provider providerDescriptors 
        /// that holds information regarding all loaded providers.</param>
        /// <param name="providerFactory">The provider factory used to create providers 
        /// as requested.</param>
        public ProviderList(IEnumerable<IProviderDescriptor> providerDescriptors, ProviderFactoryBase providerFactory)
        {
            if (providerDescriptors == null || !providerDescriptors.Any())
            {
                throw new ArgumentException("providerDescriptors not set");
            }

            if (providerFactory == null)
            {
                throw new ArgumentNullException("providerFactory");
            }

            this._providerDescriptors = providerDescriptors;
            this._providerFactory = providerFactory;
        }

        /// <summary>
        /// Initializes a new instance of Providers with the specified provider descriptors. 
        /// It internally uses the default provider factory for creating providers.
        /// </summary>
        /// <param name="providerDescriptors">A collection of provider descriptors that holds 
        /// information regarding all loaded providers.</param>
        public ProviderList(IEnumerable<IProviderDescriptor> providerDescriptors)
            : this(providerDescriptors, new ProviderFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of Providers with the specified collection of providers. 
        /// </summary>
        /// <param name="providers">The collection of providers.</param>
        public ProviderList(IEnumerable<T> providers)
        {
            this._providers = providers;
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Gets the provider with the specified name.
        /// </summary>
        /// <param name="providerName">The name of the provider.</param>
        /// <returns>The matching provider.</returns>
        public T this[string providerName]
        {
            get { return this.ByName(providerName); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all providers.
        /// </summary>
        /// <returns>The list of providers.</returns>
        public IProviderList<T> All()
        {
            return this;
        }

        /// <summary>
        /// Gets all providers in the specified group.
        /// </summary>
        /// <param name="groupName">The group of the providers.</param>
        /// <returns>The matching providers.</returns>
        public IProviderList<T> ByGroup(string groupName)
        {
            var providers =
                this.Where(x =>
                    x.Group.Equals(groupName,
                    StringComparison.OrdinalIgnoreCase))
                    .ToProviders();

            return providers;
        }

        /// <summary>
        /// Gets the provider with the specified name.
        /// </summary>
        /// <param name="providerName">The name of the provider.</param>
        /// <returns>The matching provider.</returns>
        public T ByName(string providerName)
        {
            var provider =
                this.Where(x =>
                    x.Name.Equals(providerName,
                    StringComparison.OrdinalIgnoreCase))
                    .SingleOrDefault();

            return provider;
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
            if (this._providerDescriptors != null && this._providerDescriptors.Any())
            {
                foreach (var providerDescriptor in this._providerDescriptors)
                {
                    if (typeof(T).IsAssignableFrom(providerDescriptor.ProviderType)
                        && providerDescriptor.IsEnabled)
                    {
                        yield return this._providerFactory.Create<T>(providerDescriptor);
                    }
                }
            }
            else
            {
                foreach (var provider in this._providers)
                {
                    yield return provider;
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