using System.Collections;
using System.Collections.Generic;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents the base interface for a list of Simple Providers.
    /// </summary>
    public interface ISimpleProviders : IEnumerable
    {
        /// <summary>
        /// Gets all simple providers of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of simple providers.</typeparam>
        /// <returns>The matching simple providers.</returns>
        ISimpleProviders<T> All<T>() where T : ISimpleProvider;

        /// <summary>
        /// Gets all simple providers.
        /// </summary>
        /// <returns>The list of simple providers.</returns>
        ISimpleProviders<ISimpleProvider> All();

        /// <summary>
        /// Gets the count of simple providers.
        /// </summary>
        int Count { get; }
    }

    /// <summary>
    /// Represents the base interface for a generic list of Providers.
    /// </summary>
    public interface ISimpleProviders<T> : IEnumerable<T>
        where T : ISimpleProvider
    {
        /// <summary>
        /// Gets all simple providers.
        /// </summary>
        /// <returns>The list of simple providers.</returns>
        ISimpleProviders<T> All();

        /// <summary>
        /// Gets the count of simple providers.
        /// </summary>
        int Count { get; }
    }
}
