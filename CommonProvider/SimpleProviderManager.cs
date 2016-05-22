using CommonProvider.Data;
using CommonProvider.ProviderLoaders;
using System;

namespace CommonProvider
{
    /// <summary>
    /// Represents the base interface for ISimpleProviderManager. This is 
    /// the gateway to all simple providers.
    /// </summary>
    public interface ISimpleProviderManager
    {
        /// <summary>
        /// Gets the set of loaded simple providers.
        /// </summary>
        ISimpleProviderList Providers { get; }
    }

    /// <summary>
    /// Represents the default implementation of ISimpleProviderManager. 
    /// This is the gateway to all simple providers
    /// </summary>
    public sealed class SimpleProviderManager : ISimpleProviderManager
    {
        #region Constructors


        /// <summary>
        /// Initializes an instance of SimpleProviderManager using the specified provider loader. 
        /// </summary>
        /// <param name="providerLoader">The provider loader to use in loading the providers.</param>
        public SimpleProviderManager(SimpleProviderLoaderBase providerLoader)
        {
            if (providerLoader == null)
            {
                throw new ArgumentNullException("providerLoader");
            }

            var providerTypes = providerLoader.Load();
            Providers = new SimpleProviderList(providerTypes);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the set of loaded providers.
        /// </summary>
        public ISimpleProviderList Providers { get; private set; }

        #endregion
    }
}
