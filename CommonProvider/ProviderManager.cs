﻿using CommonProvider.Data;
using CommonProvider.Factories;
using CommonProvider.ProviderLoaders;
using System;

namespace CommonProvider
{
    /// <summary>
    /// Represents the default implementation of IProviderManager. 
    /// This is the gateway to all providers and provider wide 
    /// settings.
    /// </summary>
    public sealed class ProviderManager : IProviderManager
    {
        #region Constructors

        /// <summary>
        /// Initializes an instance of ProviderManager using the specified 
        /// provider loader and provider list factory.
        /// </summary>
        /// <param name="providerLoader">The provider loader to use in loading 
        /// the providers.</param>
        /// <param name="providersFactory">The Providers factory to use 
        /// in creating the set of providers.</param>
        public ProviderManager(ProviderLoaderBase providerLoader,
            IProvidersFactory providersFactory)
        {
            if (providerLoader == null)
            {
                throw new ArgumentNullException("providerLoader");
            }

            if (providersFactory == null)
            {
                throw new ArgumentNullException("providerListFactory");
            }

            var providerData = providerLoader.Load();
            Providers = providersFactory.Create(providerData.ProviderDescriptors);
            Settings = providerData.Settings;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the set of loaded providers.
        /// </summary>
        public IProviders Providers { get; private set; }

        /// <summary>
        /// Gets all provider wide settings.
        /// </summary>
        public ISettings Settings { get; private set; }

        #endregion
    }
}
