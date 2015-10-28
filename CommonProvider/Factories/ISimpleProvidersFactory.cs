using System;
using System.Collections.Generic;
using CommonProvider.Data;

namespace CommonProvider.Factories
{
    /// <summary>
    /// Represents the base interface for a Simple Providers Factory. 
    /// It provides a means to create a Simple Providers Factory.
    /// </summary>
    public interface ISimpleProvidersFactory
    {
        /// <summary>
        /// Creates Simple Providers Factory based on the specified provider types.
        /// </summary>
        /// <param name="providerTypes">The simple providers types.</param>
        /// <returns>The created Simple Providers.</returns>
        ISimpleProviders Create(IEnumerable<Type> providerTypes);
    }
}
