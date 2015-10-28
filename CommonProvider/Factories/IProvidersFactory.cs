using CommonProvider.Data;
using System.Collections.Generic;

namespace CommonProvider.Factories
{
    /// <summary>
    /// Represents the base interface for a Providers Factory. 
    /// It provides a means to create a Providers Factory.
    /// </summary>
    public interface IProvidersFactory
    {
        /// <summary>
        /// Creates Providers based on the specified Provider Descriptors.
        /// </summary>
        /// <param name="providerDescriptors">Holds information regarding the providers.</param>
        /// <returns>The created Providers.</returns>
        IProviders Create(IEnumerable<IProviderDescriptor> providerDescriptors);
    }
}
