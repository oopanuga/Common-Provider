using System.Collections;
using System.Collections.Generic;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents the base interface for a generic list of  Zero Config Providers.
    /// </summary>
    /// <typeparam name="T">The type of Zero Config Provider.</typeparam>
    public interface IZeroConfigProviderList<out T> : IEnumerable<T>
        where T : IZeroConfigProvider
    {
        /// <summary>
        /// Gets all zero config providers.
        /// </summary>
        /// <returns>The list of zero config providers.</returns>
        IZeroConfigProviderList<T> All();

        /// <summary>
        /// Gets the count of zero config providers.
        /// </summary>
        int Count { get; }
    }

    /// <summary>
    /// Represents the base interface for a list of Zero Config Providers.
    /// </summary>
    public interface IZeroConfigProviderList : IEnumerable
    {
        /// <summary>
        /// Gets the count of zero config providers.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets all zero config providers of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of zero config providers.</typeparam>
        /// <returns>The matching zero config providers.</returns>
        IZeroConfigProviderList<T> All<T>() where T : IZeroConfigProvider;

        /// <summary>
        /// Gets all zero config providers.
        /// </summary>
        /// <returns>The list of zero config providers.</returns>
        IZeroConfigProviderList<IZeroConfigProvider> All();
    }
}