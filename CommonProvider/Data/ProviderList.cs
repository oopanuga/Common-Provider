using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommonProvider.Exceptions;

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

        /// <summary>
        /// Gets a list of Provider Descriptors
        /// </summary>
        protected readonly IEnumerable<IProviderDescriptor> ProviderDescriptors;

        /// <summary>
        /// Gets a source list of Providers
        /// </summary>
        protected readonly IEnumerable<T> Providers;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Providers with the specified provider descriptors.
        /// </summary>
        /// <param name="providerDescriptors">A collection of providerDescriptors that holds 
        /// information regarding all providers.</param>
        ///  
        public ProviderList(IEnumerable<IProviderDescriptor> providerDescriptors)
        {
            if (providerDescriptors == null || !providerDescriptors.Any())
            {
                throw new ArgumentException("providerDescriptors not set");
            }

            this.ProviderDescriptors = providerDescriptors;
        }

        /// <summary>
        /// Initializes a new instance of Providers with the specified collection of providers. 
        /// </summary>
        /// <param name="providers">The collection of providers.</param>
        public ProviderList(IEnumerable<T> providers)
        {
            this.Providers = providers;
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
                    .ToProviderList();

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
        /// Returns a generic enumerator that iterates through the Provider List.
        /// </summary>
        /// <returns>The generic enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (this.ProviderDescriptors != null && this.ProviderDescriptors.Any())
            {
                foreach (var providerDescriptor in this.ProviderDescriptors)
                {
                    if (typeof(T).IsAssignableFrom(providerDescriptor.ProviderType)
                        && providerDescriptor.IsEnabled)
                    {
                        yield return CreateProvider<T>(providerDescriptor);
                    }
                }
            }
            else
            {
                foreach (var provider in this.Providers)
                {
                    yield return provider;
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the Provider List.
        /// </summary>
        /// <returns>The generic enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #region Helpers

        /// <summary>
        /// Creates a Provider based on the specified type.
        /// </summary>
        /// <typeparam name="T">The type of provider to create.</typeparam>
        /// <param name="providerDescriptor">The provider descriptor.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">providerDescriptor</exception>
        /// <exception cref="CreateProviderException">
        /// Error creating provider
        /// </exception>
        protected T CreateProvider<T>(IProviderDescriptor providerDescriptor) where T : IProvider
        {
            try
            {
                if (providerDescriptor == null)
                {
                    throw new ArgumentNullException("providerDescriptor");
                }

                Type type = typeof(T);

                if (!type.IsAssignableFrom(providerDescriptor.ProviderType))
                {
                    throw new CreateProviderException(
                                            string.Format("{0} should be assignable from {1}.",
                                            type.Name,
                                            providerDescriptor.ProviderType.Name
                                            ));
                }

                var instance = GenericMethodInvoker.Invoke(
                                            this,
                                            "Create",
                                            providerDescriptor.ProviderType,
                                            new object[] { },
                                            BindingFlags.NonPublic | BindingFlags.Instance
                                            );

                if (instance == null)
                {
                    throw new CreateProviderException(
                        string.Format(
                        "Could not create instance of type {0}. If you've got a Provider " +
                        "implementation that exposes constructor arguments then please consider " +
                        "using any of the existing dependency resolvers or write your " +
                        "own implementation.",
                        providerDescriptor.ProviderType.Name));
                }

                var provider = (IProvider)instance;
                provider.Group = providerDescriptor.ProviderGroup;
                provider.Name = providerDescriptor.ProviderName;
                provider.Settings = providerDescriptor.ProviderSettings;

                return (T)instance;
            }
            catch (Exception ex)
            {
                if (!(ex is CreateProviderException))
                {
                    throw new CreateProviderException(
                        "Error creating provider.",
                        ex);
                }
                else
                {
                    throw;
                }
            }
        }

        protected T Create<T>() where T : IProvider
        {
            return DependencyResolver.Current.Resolve<T>();
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// Represents a list of Providers.
    /// </summary>
    public class ProviderList : ProviderList<IProvider>, IProviderList
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of Providers with the specified provider descriptors. 
        /// </summary>
        /// <param name="providerDescriptors">A collection of provider descriptors that holds 
        /// information regarding all configured providers.</param>
        public ProviderList(IEnumerable<IProviderDescriptor> providerDescriptors)
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
            return new ProviderList<T>(ProviderDescriptors);
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
                    .ToProviderList();

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