using System.Collections.Generic;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents the base interface for a generic list of Providers.
    /// </summary>
    /// <typeparam name="T">The type of Provider.</typeparam>
    public interface IProviderList<out T> : IEnumerable<T>
        where T : IProvider
    {
        /// <summary>
        /// Gets a provider with the specified name.
        /// </summary>
        /// <param name="providerName">The name of the provider.</param>
        /// <returns>The matching provider.</returns>
        T this[string providerName] { get; }

        /// <summary>
        /// Gets all providers.
        /// </summary>
        /// <returns>The list of providers.</returns>
        IProviderList<T> All();

        /// <summary>
        /// Gets all providers in the specified group.
        /// </summary>
        /// <param name="groupName">The group of the providers.</param>
        /// <returns>The matching providers.</returns>
        IProviderList<T> ByGroup(string groupName);

        /// <summary>
        /// Gets the provider with the specified name.
        /// </summary>
        /// <param name="providerName">The name of the provider.</param>
        /// <returns>The matching provider.</returns>
        T ByName(string providerName);

        /// <summary>
        /// Gets the count of providers.
        /// </summary>
        int Count { get; }
    }
}