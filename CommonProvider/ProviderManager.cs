using CommonProvider.Data;
using CommonProvider.ProviderLoaders;
using System;

namespace CommonProvider
{
    /// <summary>
    /// Represents the base interface for IProviderManager. This is 
    /// the gateway to all providers and provider wide settings.
    /// </summary>
    public interface IProviderManager
    {
        /// <summary>
        /// Gets the set of loaded providers.
        /// </summary>
        IProviderList Providers { get; }

        /// <summary>
        /// Gets all provider wide settings.
        /// </summary>
        ISettings Settings { get; }
    }

    /// <summary>
    /// Represents the default implementation of IProviderManager. 
    /// This is the gateway to all providers and provider wide 
    /// settings.
    /// </summary>
    public sealed class ProviderManager : IProviderManager
    {
        #region Constructors

        /// <summary>
        /// Initializes an instance of ProviderManager using the specified provider loader. 
        /// </summary>
        /// <param name="providerLoader">The provider loader to use in loading the providers.</param>
        public ProviderManager(ProviderLoaderBase providerLoader)
        {
            if (providerLoader == null)
            {
                throw new ArgumentNullException("providerLoader");
            }

            var providerData = providerLoader.Load();
            Providers = new ProviderList(providerData.ProviderDescriptors);
            Settings = providerData.Settings;
        }     
   
        #endregion

        #region Properties

        /// <summary>
        /// Gets the set of loaded providers.
        /// </summary>
        public IProviderList Providers { get; private set; }

        /// <summary>
        /// Gets all provider wide settings.
        /// </summary>
        public ISettings Settings { get; private set; }

        #endregion
    }
}
