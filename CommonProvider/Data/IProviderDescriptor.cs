using System;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents the base interface for a Provider Descriptor. It holds meta 
    /// data information regarding a specific loaded provider.
    /// </summary>
    public interface IProviderDescriptor
    {
        /// <summary>
        /// Gets the provider's name.
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// Gets the provider's type.
        /// </summary>
        Type ProviderType { get; }

        /// <summary>
        /// Gets the provider's group.
        /// </summary>
        string ProviderGroup { get; }

        /// <summary>
        /// Gets the provider's settings.
        /// </summary>
        ISettings ProviderSettings { get; }

        /// <summary>
        /// Gets a value indicating whether or not the provider is enabled.
        /// </summary>
        bool IsEnabled { get; }
    }
}
