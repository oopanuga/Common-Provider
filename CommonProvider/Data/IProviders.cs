using System;
using System.Collections;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents the base interface for a list of Providers.
    /// </summary>
    public interface IProviders : IEnumerable
    {
        /// <summary>
        /// Gets the count of providers.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the provider with the specified name.
        /// </summary>
        /// <param name="providerName">The name of the provider.</param>
        /// <returns>The matching provider.</returns>
        IProvider this[string providerName] { get; }

        /// <summary>
        /// Gets the provider with the specified name and type.
        /// </summary>
        /// <param name="providerName">The name of the provider.</param>
        /// <param name="type">The type of provider.</param>
        /// <returns>The matching provider.</returns>
        dynamic this[string providerName, Type type] { get; }

        /// <summary>
        /// Gets all providers of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the providers to get.</typeparam>
        /// <returns>The matching providers.</returns>
        IProviderList<T> All<T>() where T : IProvider;

        /// <summary>
        /// Gets all providers.
        /// </summary>
        /// <returns>The list of providers.</returns>
        IProviderList<IProvider> All();

        /// <summary>
        /// Gets all providers in the specified group.
        /// </summary>
        /// <param name="groupName">The group of the providers.</param>
        /// <returns>The matching providers.</returns>
        IProviderList<IProvider> ByGroup(string groupName);

        /// <summary>
        /// Gets all providers of the specified type and group.
        /// </summary>
        /// <typeparam name="T">The type of the providers.</typeparam>
        /// <param name="groupName">The group of the providers.</param>
        /// <returns>The matching providers.</returns>
        IProviderList<T> ByGroup<T>(string groupName) where T : IProvider;

        /// <summary>
        /// Gets the provider with the specified name and type.
        /// </summary>
        /// <typeparam name="T">The type of the provider.</typeparam>
        /// <param name="providerName">The name of the provider.</param>
        /// <returns>The matching provider.</returns>
        T ByName<T>(string providerName) where T : IProvider;

        /// <summary>
        /// Gets the provider with the specified name.
        /// </summary>
        /// <param name="providerName">The name of the provider.</param>
        /// <returns>The matching provider.</returns>
        IProvider ByName(string providerName);
    }
}
