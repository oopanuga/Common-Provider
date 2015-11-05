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
    public class Providers : ProviderList<IProvider>, IProviders
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
                    "ByName",
                    type,
                    new object[] { providerName },
                    BindingFlags.Public | BindingFlags.Instance
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
        public IProviderList<T> All<T>() where T : IProvider
        {
            return new ProviderList<T>(_providerDescriptors, _providerFactory);
        }

        /// <summary>
        /// Gets all providers of the specified type and group.
        /// </summary>
        /// <typeparam name="T">The type of the providers.</typeparam>
        /// <param name="groupName">The group of the providers.</param>
        /// <returns>The matching providers.</returns>
        public IProviderList<T> ByGroup<T>(string groupName) where T : IProvider
        {
            var providers =
                All<T>().Where(x =>
                    x.Group.Equals(groupName,
                    StringComparison.OrdinalIgnoreCase))
                    .ToProviders();

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
                    x.Name.Equals(providerName, 
                    StringComparison.OrdinalIgnoreCase))
                    .SingleOrDefault();

            return provider;
        }

        #endregion
    }
}
