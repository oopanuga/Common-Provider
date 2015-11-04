using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
