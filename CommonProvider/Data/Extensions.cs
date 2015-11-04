using System.Collections.Generic;

namespace CommonProvider.Data
{
    public static class Extensions
    {
        public static IProviders<T> ToProviders<T>(this IEnumerable<T> providerSource)
            where T : IProvider
        {
            return new Providers<T>(providerSource);
        }
    }
}
