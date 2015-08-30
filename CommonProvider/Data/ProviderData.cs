using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents the default implementation of IProviderData. It holds information 
    /// regarding all loaded providers.
    /// </summary>
    public class ProviderData : IProviderData
    {
        /// <summary>
        /// Initializes a new instance of ProviderData with a specified collection 
        /// of provider descriptors and provider wide settings.
        /// </summary>
        /// <param name="providerDescriptors">It holds information regarding the 
        /// loaded providers.</param>
        /// <param name="settings">The provider wide settings.</param>
        public ProviderData(
            IEnumerable<IProviderDescriptor> providerDescriptors, ISettings settings)
        {
            if (providerDescriptors == null || 
                !providerDescriptors.Any())
            {
                throw new ArgumentException(
                    "providerDescriptors not set");
            }

            Settings = settings;
            ProviderDescriptors = providerDescriptors;
        }

        /// <summary>
        /// Gets provider wide settings.
        /// </summary>
        public ISettings Settings { get; private set; }

        /// <summary>
        /// Gets information regarding all loaded providers.
        /// </summary>
        public IEnumerable<IProviderDescriptor> ProviderDescriptors { get; private set; }
    }
}
