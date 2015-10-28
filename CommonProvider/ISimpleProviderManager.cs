using CommonProvider.Data;

namespace CommonProvider
{
    /// <summary>
    /// Represents the base interface for ISimpleProviderManager. This is 
    /// the gateway to all simple providers.
    /// </summary>
    public interface ISimpleProviderManager
    {
        /// <summary>
        /// Gets the set of loaded simple providers.
        /// </summary>
        ISimpleProviders Providers { get; }
    }
}
