using CommonProvider.Factories;
using CommonProvider.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents a set of Providers.
    /// </summary>
    public class Providers : IProviders
    {
        #region Fields

        IEnumerable<IProviderDescriptor> _providerDescriptors;
        ProviderFactoryBase _providerFactory;

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

        #endregion

        #region Indexers

        /// <summary>
        /// Gets a provider with the specified name.
        /// </summary>
        /// <param name="providerName">The name of the provider to get.</param>
        /// <returns>The matching provider.</returns>
        public IProvider this[string providerName]
        {
            get
            {
                return ByName(providerName);
            }
        }

        /// <summary>
        /// Gets a provider with the specified name.
        /// </summary>
        /// <param name="providerName">The name of the provider to get.</param>
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

        #region Properties

        /// <summary>
        /// Gets the count of providers
        /// </summary>
        public int Count
        {
            get
            {
                return this.All<IProvider>().Count();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all providers of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the providers to get.</typeparam>
        /// <returns>The matching providers.</returns>
        public IEnumerable<T> All<T>() where T : IProvider
        {
            var providers =
                _providerDescriptors
                .Where(x =>
                    typeof(T).IsAssignableFrom(x.ProviderType)
                    && x.IsEnabled)
                    .Select(x => _providerFactory.Create<T>(x));
                
            return providers;
        }

        /// <summary>
        /// Gets all providers.
        /// </summary>
        /// <returns>The providers.</returns>
        public IEnumerable<IProvider> All()
        {
            return this.All<IProvider>();
        }

        /// <summary>
        /// Gets providers that belong to the specified group.
        /// </summary>
        /// <param name="groupName">The group name of the providers to get.</param>
        /// <returns>The matching providers.</returns>
        public IEnumerable<IProvider> ByGroup(string groupName)
        {
            var providers = this.All<IProvider>()
                .Where(x => 
                    x.Group.Equals(groupName, 
                    StringComparison.OrdinalIgnoreCase));

            return providers;
        }

        /// <summary>
        /// Gets providers that belong to the specified group and are of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the providers to get.</typeparam>
        /// <param name="groupName">The group name of the providers to get.</param>
        /// <returns>The matching providers.</returns>
        public IEnumerable<T> ByGroup<T>(string groupName) where T : IProvider
        {
            var providers = this.All<T>()
                .Where(x => 
                x.Group.Equals(groupName, 
                StringComparison.OrdinalIgnoreCase));

            return providers;
                
        }

        /// <summary>
        /// Gets a provider with the specified name and of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the providers to get.</typeparam>
        /// <param name="providerName">The name of the provider to get.</param>
        /// <returns>The matching provider.</returns>
        public T ByName<T>(string providerName) where T : IProvider
        {
            return GetByName<T>(providerName);
        }

        /// <summary>
        /// Gets a provider with the specified name.
        /// </summary>
        /// <param name="providerName">The name of the provider to get.</param>
        /// <returns>The matching provider.</returns>
        public IProvider ByName(string providerName)
        {
            return this.GetByName<IProvider>(providerName);
        }

        #endregion

        #region Helpers

        private T GetByName<T>(string providerName) where T : IProvider
        {
            var provider =
                _providerDescriptors
                .Where(x =>
                    typeof(T).IsAssignableFrom(x.ProviderType)
                    && x.IsEnabled
                    && x.ProviderName
                    .Equals(providerName,
                    StringComparison.OrdinalIgnoreCase))
                    .Select(x => _providerFactory.Create<T>(x))
                .SingleOrDefault();

            return provider;
        }

        #endregion
    }
}
