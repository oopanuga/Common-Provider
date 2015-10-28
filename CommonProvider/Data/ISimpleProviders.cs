using System.Collections.Generic;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents the base interface for a set of Simple Providers.
    /// </summary>
    public interface ISimpleProviders
    {
        /// <summary>
        /// Gets all simple providers of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of simple providers to get.</typeparam>
        /// <returns>The matching simple providers.</returns>
        IEnumerable<T> All<T>() where T : ISimpleProvider;

        /// <summary>
        /// Gets all simple providers.
        /// </summary>
        /// <returns>The simple providers.</returns>
        IEnumerable<ISimpleProvider> All();

        /// <summary>
        /// Gets the count of simple providers
        /// </summary>
        int Count { get; }
    }
}
