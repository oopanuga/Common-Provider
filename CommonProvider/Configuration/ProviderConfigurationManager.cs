using System.Configuration;

namespace CommonProvider.Configuration
{
    /// <summary>
    /// Represents the default implementation of IProviderConfigurationManager.
    /// </summary>
    public class ProviderConfigurationManager : IProviderConfigurationManager
    {
        /// <summary>
        /// Gets the section in the configuration file that has the 
        /// CommonProvider's configuration.
        /// </summary>
        /// <param name="sectionName">The name of CommonProvider's 
        /// configuration section.</param>
        /// <returns>The configuration section</returns>
        public ProviderConfigSection GetSection(string sectionName)
        {
            return ConfigurationManager.GetSection(sectionName) as ProviderConfigSection;
        }
    }
}
