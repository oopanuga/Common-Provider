
using CommonProvider.Data;
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
        IProviders Providers { get; }

        /// <summary>
        /// Gets all provider wide settings.
        /// </summary>
        ISettings Settings { get; }
    }
}
