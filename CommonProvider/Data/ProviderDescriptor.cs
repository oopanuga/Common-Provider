﻿using System;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents the default implementation of IProviderDescriptor. It holds meta 
    /// data information regarding a specific loaded provider.
    /// </summary>
    public class ProviderDescriptor : IProviderDescriptor
    {
        #region Constructors

        /// <summary>
        /// Initializes an instance of ProviderDescriptor using the specified provider details.
        /// </summary>
        /// <param name="providerName">The provider's name.</param>
        /// <param name="providerGroup">The provider's group.</param>
        /// <param name="providerType">The provider's type.</param>
        /// <param name="providerSettings">The provider's settings.</param>
        /// <param name="isEnabled">A value indicating whether or not the provider has been enabled.</param>
        public ProviderDescriptor(string providerName, string providerGroup,
            Type providerType, ISettings providerSettings, bool isEnabled)
        {
            if (providerType == null)
            {
                throw new ArgumentNullException("providerType");
            }

            ProviderName = providerName;
            ProviderGroup = providerGroup;
            ProviderType = providerType;
            ProviderSettings = providerSettings;
            IsEnabled = isEnabled;
        } 

        #endregion

        #region Properties

        /// <summary>
        /// Gets a providers's name.
        /// </summary>
        public string ProviderName { get; private set; }

        /// <summary>
        /// Gets a provider's type.
        /// </summary>
        public Type ProviderType { get; private set; }

        /// <summary>
        /// Gets a provider's group.
        /// </summary>
        public string ProviderGroup { get; private set; }

        /// <summary>
        /// Gets a provider's settings.
        /// </summary>
        public ISettings ProviderSettings { get; private set; }

        /// <summary>
        /// Gets a value indicating if the provider is enabled or not.
        /// </summary>
        public bool IsEnabled { get; private set; }

        #endregion
    }
}
