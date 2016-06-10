using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents the base interface for ProviderConfig. It holds information 
    /// regarding all configured providers.
    /// </summary>
    public interface IProviderConfig
    {
        /// <summary>
        /// Gets provider wide settings.
        /// </summary>
        IProviderSettings Settings { get; }

        /// <summary>
        /// Gets the meta data of a configured Provider e.g. Name, Type etc.
        /// </summary>
        IEnumerable<IProviderDescriptor> ProviderDescriptors { get; }
    }

    /// <summary>
    /// Represents the default implementation of IProviderData. It holds information 
    /// regarding all configured providers.
    /// </summary>
    public class ProviderConfig : IProviderConfig
    {
        /// <summary>
        /// Initializes a new instance of ProviderConfig with a specified collection 
        /// of provider descriptors and provider wide settings.
        /// </summary>
        /// <param name="providerDescriptors">It holds information regarding the providers.</param>
        /// <param name="settings">The provider wide settings.</param>
        public ProviderConfig(
            IEnumerable<IProviderDescriptor> providerDescriptors, IProviderSettings settings)
        {
            if (providerDescriptors == null || !providerDescriptors.Any())
            {
                throw new ArgumentException("providerDescriptors not set.");
            }

            Settings = settings;
            ProviderDescriptors = providerDescriptors;
        }

        /// <summary>
        /// Gets provider wide settings.
        /// </summary>
        public IProviderSettings Settings { get; private set; }

        /// <summary>
        /// Gets information regarding all configured providers.
        /// </summary>
        public IEnumerable<IProviderDescriptor> ProviderDescriptors { get; private set; }
    }
}
