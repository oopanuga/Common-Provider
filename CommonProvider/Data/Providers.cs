using CommonProvider.Factories;
using CommonProvider.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents a list of Providers.
    /// </summary>
    public class Providers : Providers<IProvider>, IProviders
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of Providers with the specified provider 
        /// descriptors and a provider factory.
        /// </summary>
        /// <param name="providerDescriptors">A collection of provider providerDescriptors 
        /// that holds information regarding all loaded providers.</param>
        /// <param name="providerFactory">The provider factory used to create providers 
        /// as requested.</param>
        public Providers(IEnumerable<IProviderDescriptor> providerDescriptors,
            ProviderFactoryBase providerFactory)
            : base(providerDescriptors, providerFactory)
        {
        }

        /// <summary>
        /// Initializes a new instance of Providers with the specified provider descriptors. 
        /// It internally uses the default provider factory for creating providers.
        /// </summary>
        /// <param name="providerDescriptors">A collection of provider descriptors that holds 
        /// information regarding all loaded providers.</param>
        public Providers(IEnumerable<IProviderDescriptor> providerDescriptors)
            : base(providerDescriptors)
        {
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Gets the provider with the specified name and type.
        /// </summary>
        /// <param name="providerName">The name of the provider.</param>
        /// <param name="type">The type of provider.</param>
        /// <returns>The matching provider.</returns>
        public dynamic this[string providerName, Type type]
        {
            get
            {
                return GenericMethodInvoker.Invoke(
                    this,
                    "GetByName",
                    type,
                    new object[] { providerName },
                    BindingFlags.NonPublic | BindingFlags.Instance
                    );
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all providers of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the providers to get.</typeparam>
        /// <returns>The matching providers.</returns>
        public IProviders<T> All<T>() where T : IProvider
        {
            return new Providers<T>(_providerDescriptors, _providerFactory);
        }

        /// <summary>
        /// Gets all providers of the specified type and group.
        /// </summary>
        /// <typeparam name="T">The type of the providers.</typeparam>
        /// <param name="groupName">The group of the providers.</param>
        /// <returns>The matching providers.</returns>
        public IProviders<T> ByGroup<T>(string groupName) where T : IProvider
        {
            var providers =
                All<T>()
                .Where(x =>
                x.Group.Equals(groupName,
                StringComparison.OrdinalIgnoreCase))
                .ToProviders<T>();

            return providers;
        }

        /// <summary>
        /// Gets the provider with the specified name and type.
        /// </summary>
        /// <typeparam name="T">The type of the provider.</typeparam>
        /// <param name="providerName">The name of the provider.</param>
        /// <returns>The matching provider.</returns>
        public T ByName<T>(string providerName) where T : IProvider
        {
            var provider =
                All<T>().Where(x =>
                    x.Name
                    .Equals(providerName,
                    StringComparison.OrdinalIgnoreCase))
                .SingleOrDefault();

            return provider;
        }

        #endregion

        #region Helpers

        private T GetByName<T>(string providerName) where T : IProvider
        {
            var provider =
               All<T>().Where(x =>
                   x.Name
                   .Equals(providerName,
                   StringComparison.OrdinalIgnoreCase))
               .SingleOrDefault();

            return provider;
        }

        #endregion
    }

    /// <summary>
    /// Represents a generic list of Providers.
    /// </summary>
    public class Providers<T> : IProviders<T>
        where T : IProvider
    {
        #region Fields

        protected readonly IEnumerable<IProviderDescriptor> _providerDescriptors;
        protected readonly ProviderFactoryBase _providerFactory;
        readonly IEnumerable<T> _providers;

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
        public Providers(IEnumerable<IProviderDescriptor> providerDescriptors,
            ProviderFactoryBase providerFactory)
        {
            if (providerDescriptors == null || !providerDescriptors.Any())
            {
                throw new ArgumentException("providerDescriptors not set");
            }

            if (providerFactory == null)
            {
                throw new ArgumentNullException("providerFactory");
            }

            _providerDescriptors = providerDescriptors;
            _providerFactory = providerFactory;
        }

        /// <summary>
        /// Initializes a new instance of Providers with the specified provider descriptors. 
        /// It internally uses the default provider factory for creating providers.
        /// </summary>
        /// <param name="providerDescriptors">A collection of provider descriptors that holds 
        /// information regarding all loaded providers.</param>
        public Providers(IEnumerable<IProviderDescriptor> providerDescriptors)
            : this(providerDescriptors, new ProviderFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of Providers with the specified collection of providers. 
        /// </summary>
        /// <param name="providers">The collection of providers.</param>
        public Providers(IEnumerable<T> providers)
        {
            //if (providers == null || !providers.Any())
            //{
            //    throw new ArgumentException("providers not set");
            //}

            _providers = providers;
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Gets a provider with the specified name.
        /// </summary>
        /// <param name="providerName">The name of the provider.</param>
        /// <returns>The matching provider.</returns>
        public T this[string providerName]
        {
            get { return ByName(providerName); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all providers.
        /// </summary>
        /// <returns>The list of providers.</returns>
        public IProviders<T> All()
        {
            return this;
        }

        /// <summary>
        /// Gets all providers in the specified group.
        /// </summary>
        /// <param name="groupName">The group of the providers.</param>
        /// <returns>The matching providers.</returns>
        public IProviders<T> ByGroup(string groupName)
        {
            var providers =
                this.Where(x =>
                x.Group.Equals(groupName,
                StringComparison.OrdinalIgnoreCase))
                .ToProviders<T>();

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
                this
                .Where(x =>
                    x.Name.Equals(providerName, StringComparison.OrdinalIgnoreCase))
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
            if (_providerDescriptors != null && _providerDescriptors.Any())
            {
                foreach (var providerDescriptor in _providerDescriptors)
                {
                    if (typeof(T).IsAssignableFrom(providerDescriptor.ProviderType)
                        && providerDescriptor.IsEnabled)
                    {
                        yield return _providerFactory.Create<T>(providerDescriptor);
                    }
                }
            }
            else
            {
                foreach (var provider in _providers)
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
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
