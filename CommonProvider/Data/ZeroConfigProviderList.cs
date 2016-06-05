using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommonProvider.Exceptions;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents a generic list of Zero Config Providers.
    /// </summary>
    /// <typeparam name="T">The type of Zero Config Provider.</typeparam>
    public class ZeroConfigProviderList<T> : IZeroConfigProviderList<T>
        where T : IZeroConfigProvider
    {
        #region Fields

        /// <summary>
        /// Gets a list of Provider Types
        /// </summary>
        protected readonly IEnumerable<Type> ProviderTypes;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Zero Config Providers with the specified provider types. 
        /// It internally uses the default provider factory for creating providers.
        /// </summary>
        /// <param name="providerTypes">A collection of zero config provider types.</param>
        public ZeroConfigProviderList(IEnumerable<Type> providerTypes)
        {
            if (providerTypes == null || !providerTypes.Any())
            {
                throw new ArgumentException("providerTypes not set");
            }

            this.ProviderTypes = providerTypes;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all zero config providers.
        /// </summary>
        /// <returns>The list of zero config providers.</returns>
        public IZeroConfigProviderList<T> All()
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
        /// Returns a generic enumerator that iterates through the Zero Config Provider List.
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
                        yield return CreateZeroConfigProvider<T>(_providerType);
                    }
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the Zero Config Provider List.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #region Helpers

        /// <summary>
        /// Creates a Zero Config Provider based on the specified type.
        /// </summary>
        /// <typeparam name="T">The type of provider to create.</typeparam>
        /// <param name="providerType">The type of zero config provider.</param>
        /// <returns>The created Zero Config Provider.</returns>
        protected T CreateZeroConfigProvider<T>(Type providerType) where T : IZeroConfigProvider
        {
            try
            {
                if (providerType == null)
                {
                    throw new ArgumentNullException("providerType");
                }

                Type type = typeof(T);

                if (!type.IsAssignableFrom(providerType))
                {
                    throw new CreateProviderException(
                                            string.Format("{0} should be assignable from {1}",
                                            type.Name,
                                            providerType.Name
                                            ));
                }

                var instance = GenericMethodInvoker.Invoke(
                                            this,
                                            "Create",
                                            providerType,
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
                        "own implementation(see documentation for details)",
                        providerType.Name));
                }

                return (T)instance;
            }
            catch (Exception ex)
            {
                if (!(ex is CreateProviderException))
                {
                    throw new CreateProviderException(
                        "Error creating provider",
                        ex);
                }
                else
                {
                    throw;
                }
            }
        }

        protected T Create<T>() where T : IZeroConfigProvider
        {
            return DependencyResolver.Current.Resolve<T>();
        } 

        #endregion

        #endregion
    }

    /// <summary>
    /// Represents a list of Zero Config Providers.
    /// </summary>
    public class ZeroConfigProviderList : ZeroConfigProviderList<IZeroConfigProvider>, IZeroConfigProviderList
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of Zero Config Providers with the specified provider types. 
        /// </summary>
        /// <param name="providerTypes">A collection of zero config provider types.</param>
        public ZeroConfigProviderList(IEnumerable<Type> providerTypes)
            : base(providerTypes)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all zero config providers of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the zero config providers.</typeparam>
        /// <returns>The matching zero config providers.</returns>
        public IZeroConfigProviderList<T> All<T>() where T : IZeroConfigProvider
        {
            return new ZeroConfigProviderList<T>(ProviderTypes);
        }

        #endregion
    }
}