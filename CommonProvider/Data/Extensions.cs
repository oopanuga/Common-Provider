using System.Collections.Generic;

namespace CommonProvider.Data
{
    /// <summary>
    /// Extensions for Common Provider data bits
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Gets an instance of ProviderList that takes an IEnumerable list of providers.
        /// </summary>
        /// <typeparam name="T">The type of the provider.</typeparam>
        /// <param name="providers"></param>
        /// <returns>The Provider List.</returns>
        public static IProviderList<T> ToProviderList<T>(this IEnumerable<T> providers)
            where T : IProvider
        {
            return new ProviderList<T>(providers);
        }
    }
}
