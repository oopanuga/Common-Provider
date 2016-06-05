using CommonProvider.Data;
using System;
using CommonProvider.ConfigSources;
using CommonProvider.ConfigSources.Xml;

namespace CommonProvider
{
    /// <summary>
    /// Represents the base interface for IProviderManager. This is 
    /// the gateway to all providers and provider wide settings.
    /// </summary>
    public interface IProviderManager
    {
        /// <summary>
        /// Gets a list of configured providers.
        /// </summary>
        IProviderList Providers { get; }

        /// <summary>
        /// Gets all provider wide settings.
        /// </summary>
        IProviderSettings Settings { get; }
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
        /// Initializes an instance of ProviderManager using the specified provider config source. 
        /// </summary>
        /// <param name="providerConfigSource">The provider config source to get provider configuration from.</param>
        public ProviderManager(ProviderConfigSource providerConfigSource)
        {
            if (providerConfigSource == null)
            {
                throw new ArgumentNullException("providerConfigSource");
            }

            var providerConfig = providerConfigSource.GetProviderConfiguration();
            Providers = new ProviderList(providerConfig.ProviderDescriptors);
            Settings = providerConfig.Settings;
        }

        /// <summary>
        /// Initializes an instance of ProviderManager using the XmlProviderConfigSource
        /// </summary>
        public ProviderManager():this(new XmlProviderConfigSource())
        {
        }  
   
        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of configured providers.
        /// </summary>
        public IProviderList Providers { get; private set; }

        /// <summary>
        /// Gets all provider wide settings.
        /// </summary>
        public IProviderSettings Settings { get; private set; }

        #endregion
    }
}
