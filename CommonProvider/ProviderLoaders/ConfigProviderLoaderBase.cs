using CommonProvider.Data;
using CommonProvider.Exceptions;
using System;

namespace CommonProvider.ProviderLoaders
{
    /// <summary>
    /// Represents the base class for a Provider Loader.
    /// </summary>
    public abstract class ConfigProviderLoaderBase
    {
        /// <summary>
        /// Loads provider information/meta data e.g. Type, Name etc. 
        /// This method is visible to only classes that derive from 
        /// ConfigProviderLoader. Its is called internally by the public 
        /// Load() method.
        /// </summary>
        /// <returns>The loaded data for providers.</returns>
        protected abstract IProviderData PerformLoad();

        /// <summary>
        /// Loads provider information/meta data e.g. Type, Name etc.
        /// </summary>
        /// <returns>The loaded providers data.</returns>
        internal IProviderData Load()
        {
            try
            {
                var providerData = PerformLoad();

                if (providerData == null)
                {
                    throw new ProviderLoadException(
                        "providerData not set");
                }

                return providerData;
            }
            catch (Exception ex)
            {
                if (!(ex is ProviderLoadException))
                {
                    throw new ProviderLoadException(
                        "Error loading providers", ex);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
