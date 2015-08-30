using System;
using System.Collections.Generic;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents the base interface for a set of Providers.
    /// </summary>
    public interface IProviders
    {
        /// <summary>
        /// Gets a provider with the specified name.
        /// </summary>
        /// <param name="providerName">The name of the provider to get.</param>
        /// <returns>The matching provider.</returns>
        IProvider this[string providerName] { get; }

        /// <summary>
        /// Gets a provider with the specified name.
        /// </summary>
        /// <param name="providerName">The name of the provider to get.</param>
        /// <param name="type">The type of provider.</param>
        /// <returns>The matching provider.</returns>
        dynamic this[string providerName, Type type] { get; }

        /// <summary>
        /// Gets all providers of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the providers to get.</typeparam>
        /// <returns>The matching providers.</returns>
        IEnumerable<T> All<T>() where T : IProvider;

        /// <summary>
        /// Gets all providers.
        /// </summary>
        /// <returns>The providers.</returns>
        IEnumerable<IProvider> All();

        /// <summary>
        /// Gets providers that belong to the specified group.
        /// </summary>
        /// <param name="groupName">The group name of the providers to get.</param>
        /// <returns>The matching providers.</returns>
        IEnumerable<IProvider> ByGroup(string groupName);

        /// <summary>
        /// Gets providers that belong to the specified group and are of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the providers to get.</typeparam>
        /// <param name="groupName">The group name of the providers to get.</param>
        /// <returns>The matching providers.</returns>
        IEnumerable<T> ByGroup<T>(string groupName) where T : IProvider;

        /// <summary>
        /// Gets a provider with the specified name and of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the providers to get.</typeparam>
        /// <param name="providerName">The name of the provider to get.</param>
        /// <returns>The matching provider.</returns>
        T ByName<T>(string providerName) where T : IProvider;

        /// <summary>
        /// Gets a provider with the specified name.
        /// </summary>
        /// <param name="providerName">The name of the provider to get.</param>
        /// <returns>The matching provider.</returns>
        IProvider ByName(string providerName);

        /// <summary>
        /// Gets the count of providers
        /// </summary>
        int Count { get; }
    }
}
