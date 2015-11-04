using CommonProvider.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonProvider.ProviderLoaders
{
    /// <summary>
    /// Represents the base class for a Simple Provider Loader.
    /// </summary>
    public abstract class SimpleProviderLoaderBase
    {
        /// <summary>
        /// Loads simple provider types. This method is visible to only classes 
        /// that derive from SimpleProviderLoaderBase. Its is called internally 
        /// by the public Load() method.
        /// </summary>
        /// <returns>The loaded simple provider types.</returns>
        protected abstract IEnumerable<Type> PerformLoad();

        /// <summary>
        /// Loads simple provider types.
        /// </summary>
        /// <returns>The loaded simple providers types.</returns>
        internal IEnumerable<Type> Load()
        {
            try
            {
                var providerTypes = PerformLoad();

                if (providerTypes == null || !providerTypes.Any())
                {
                    throw new ProviderLoadException(
                        "providerTypes not set");
                }

                return providerTypes;
            }
            catch (Exception ex)
            {
                if (!(ex is ProviderLoadException))
                {
                    throw new ProviderLoadException(
                        "Error loading providerTypes", ex);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
