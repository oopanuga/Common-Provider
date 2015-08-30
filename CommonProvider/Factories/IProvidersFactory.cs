using CommonProvider.Data;
using System.Collections.Generic;

namespace CommonProvider.Factories
{
    /// <summary>
    /// Represents the base interface for a Provider List Factory. 
    /// It provides a means to create a Provider List.
    /// </summary>
    public interface IProvidersFactory
    {
        /// <summary>
        /// Creates a Provider List based on the specified Provider Descriptors.
        /// </summary>
        /// <param name="providerDescriptors">Holds information regarding the providers.</param>
        /// <returns>The Provider List.</returns>
        IProviders Create(IEnumerable<IProviderDescriptor> providerDescriptors);
    }
}
