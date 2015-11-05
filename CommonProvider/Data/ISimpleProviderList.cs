using System.Collections.Generic;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents the base interface for a generic list of Providers.
    /// </summary>
    /// <typeparam name="T">The type of Simple Provider.</typeparam>
    public interface ISimpleProviderList<out T> : IEnumerable<T>
        where T : ISimpleProvider
    {
        /// <summary>
        /// Gets all simple providers.
        /// </summary>
        /// <returns>The list of simple providers.</returns>
        ISimpleProviderList<T> All();

        /// <summary>
        /// Gets the count of simple providers.
        /// </summary>
        int Count { get; }
    }
}