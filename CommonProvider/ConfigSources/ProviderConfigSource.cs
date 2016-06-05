using System;
using CommonProvider.Data;
using CommonProvider.Exceptions;

namespace CommonProvider.ConfigSources
{
    /// <summary>
    /// Represents the base class for a Provider Configuration Source.
    /// </summary>
    public abstract class ProviderConfigSource
    {
        /// <summary>
        ///  Gets provider configuration from a configuration source.
        /// </summary>
        /// <returns>The provider configuration.</returns>
        protected abstract IProviderConfig GetProviderConfig();

        /// <summary>
        /// Gets provider configuration from a configuration source.
        /// </summary>
        /// <returns>The provider configuration.</returns>
        internal IProviderConfig GetProviderConfiguration()
        {
            try
            {
                var providerConfig = GetProviderConfig();

                if (providerConfig == null)
                {
                    throw new GetProviderConfigException("No provider config returned");
                }

                return providerConfig;
            }
            catch (Exception ex)
            {
                if (!(ex is GetProviderConfigException))
                {
                    throw new GetProviderConfigException(
                        "Error getting provider config", ex);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
